﻿using AircraftDataAnalysisWcfService.DALs;
using FlightDataEntities;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AircraftDataAnalysisWcfService
{
    public class DataInputServiceBll
    {
        /// <summary>
        /// 添加或更新一个架次信息
        /// 架次是实体，用Common库，Collection也可以直接取
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        internal FlightDataEntities.Flight AddOrReplaceFlight(
            FlightDataEntities.Flight flight)
        {
            Flight flightResult = null;
            using (AircraftMongoDbDal dal = new AircraftMongoDbDal())
            {
                MongoServer mongoServer = dal.GetMongoServer();
                //不用判断是否为空，必须不能为空才能继续，否则内部要抛异常
                try
                {
                    MongoDatabase database = dal.GetMongoDatabaseCommon(mongoServer);
                    //这是实体，直接取Common吧
                    //dal.GetMongoDatabaseByAircraftModel(mongoServer, flight.Aircraft.AircraftModel);

                    if (database != null)
                    {
                        MongoCollection<Flight> modelCollection
                            = database.GetCollection<Flight>(AircraftMongoDb.COLLECTION_FLIGHT);

                        flightResult = InsertOrUpdateByFlightID(flight, flightResult, modelCollection);
                    }
                }
                catch (Exception e)
                {
                    LogHelper.Error("AddOrReplaceFlight", e);
                    flightResult = null;
                }
            }

            return flightResult;
        }

        /// <summary>
        /// 根据FlightID插入或更新一个架次信息，插入的是实体，使用Common库
        /// </summary>
        /// <param name="flight"></param>
        /// <param name="flightResult">flightResult才是真正要返回的结果，因为flight如果是需要新增的，则不会有MongoDB需要的ObjectId</param>
        /// <param name="modelCollection"></param>
        /// <returns></returns>
        private static Flight InsertOrUpdateByFlightID(
            FlightDataEntities.Flight flight, Flight flightResult, MongoCollection<Flight> modelCollection)
        {
            IMongoQuery q1 = Query.EQ("FlightID", new MongoDB.Bson.BsonString(flight.FlightID));

            var cursor = modelCollection.Find(q1);
            //第一次查询，是判断是否需要UPDATE
            if (cursor != null && cursor.Count() > 0)
            {
                foreach (var value in cursor.AsEnumerable())
                {
                    value.FlightName = flight.FlightName;
                    value.Aircraft = flight.Aircraft;
                    value.EndSecond = flight.EndSecond;
                    value.StartSecond = flight.StartSecond;
                    modelCollection.Save(value);

                    flightResult = value;
                }
            }
            else
            {//如果INSERT，就必须插入之后才有ObjectId，需要返回带有ObjectId的对象（不单单只考虑WCF返回没带有ObjectId的情况）
                modelCollection.Insert(flight);

                var cursor2 = modelCollection.Find(q1);
                if (cursor2 != null && cursor2.Count() > 0)
                {
                    flightResult = cursor2.First();
                }
                else flightResult = null;
            }
            return flightResult;
        }

        /// <summary>
        /// 删除的是实体，需要使用Common数据库。后面相关记录则根据分表原则找到数据库和Collection
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        internal string DeleteExistsData(FlightDataEntities.Flight flight)
        {
            using (AircraftMongoDbDal dal = new AircraftMongoDbDal())
            {
                MongoServer mongoServer = dal.GetMongoServer();
                //不用判断是否为空，必须不能为空才能继续，否则内部要抛异常
                try
                {
                    MongoDatabase database = mongoServer.GetDatabase(AircraftMongoDb.DATABASE_COMMON);
                    if (database != null)
                    {
                        MongoCollection<Flight> modelCollection
                            = database.GetCollection<Flight>(AircraftMongoDb.COLLECTION_FLIGHT);
                        //删除架次
                        IMongoQuery q1 = Query.EQ("FlightID", new MongoDB.Bson.BsonString(flight.FlightID));
                        var writeResult = modelCollection.Remove(q1);

                        //TODO:删除相关记录
                        this.RemoveRelatedRecords(mongoServer, flight, dal, q1);
                    }
                }
                catch (Exception e)
                {
                    LogHelper.Error("DeleteExistsData", e);
                    return e.Message;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 删除一个架次相关记录，这是记录，需要重新定位数据库和Collection
        /// </summary>
        /// <param name="mongoServer"></param>
        /// <param name="flight"></param>
        private void RemoveRelatedRecords(MongoServer mongoServer, Flight flight, AircraftMongoDbDal dal, IMongoQuery flightIdQuery)
        {//TODO: 
            try
            {//此方法操作的记录为跟架次密切相关，需要拆分存储的记录，最好在DAL里面去处理表名构建逻辑
                MongoDatabase database = dal.GetMongoDatabaseByAircraftModel(mongoServer, flight.Aircraft.AircraftModel);
                if (database != null)
                {
                    //删除判据记录
                    MongoCollection<FlightDataEntities.Decisions.DecisionRecord> modelCollection
                        = dal.GetDecisionRecordMongoCollectionByFlight(database, flight);
                    modelCollection.Remove(flightIdQuery);

                    //删除Level1记录
                    MongoCollection<FlightDataEntities.Level1FlightRecord> modelCollection1
                        = dal.GetLevel1FlightRecordMongoCollectionByFlight(database, flight);
                    modelCollection1.Remove(flightIdQuery);

                    //删除LevelTop记录
                    MongoCollection<FlightDataEntities.LevelTopFlightRecord> modelCollection2
                        = dal.GetLevelTopFlightRecordMongoCollectionByFlight(database, flight);
                    modelCollection2.Remove(flightIdQuery);

                    ////删除Level2记录
                    //MongoCollection<FlightDataEntities.Level2FlightRecord> modelCollection2
                    //    = dal.GetLevel2FlightRecordMongoCollectionByFlight(database, flight);
                    //modelCollection2.Remove(flightIdQuery);
                }
            }
            catch (Exception e)
            {
                LogHelper.Error("RemoveRelatedRecords", e);
            }
        }

        /// <summary>
        /// 添加判据记录，这是记录，需要重新定位数据库和Collection
        /// </summary>
        /// <param name="flight"></param>
        /// <param name="records"></param>
        /// <returns></returns>
        internal string AddDecisionRecordsBatch(FlightDataEntities.Flight flight,
            FlightDataEntities.Decisions.DecisionRecord[] records)
        {
            using (AircraftMongoDbDal dal = new AircraftMongoDbDal())
            {
                MongoServer mongoServer = dal.GetMongoServer();
                //不用判断是否为空，必须不能为空才能继续，否则内部要抛异常
                try
                {//此方法操作的记录为跟架次密切相关，需要拆分存储的记录，最好在DAL里面去处理表名构建逻辑
                    MongoDatabase database = dal.GetMongoDatabaseByAircraftModel(mongoServer, flight.Aircraft.AircraftModel);
                    if (database != null)
                    {
                        MongoCollection<FlightDataEntities.Decisions.DecisionRecord> modelCollection
                            = dal.GetDecisionRecordMongoCollectionByFlight(database, flight);

                        modelCollection.InsertBatch(records);
                    }
                }
                catch (Exception e)
                {
                    LogHelper.Error("AddDecisionRecordsBatch", e);
                    return e.Message;
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 添加Level1、Level2数据点记录，这是记录，需要重新定位数据库和Collection
        /// </summary>
        /// <param name="flight"></param>
        /// <param name="parameterID"></param>
        /// <param name="reducedRecords"></param>
        /// <param name="level2Record"></param>
        /// <returns></returns>
        internal string AddOneParameterValue(FlightDataEntities.Flight flight, string parameterID,
            FlightDataEntities.Level1FlightRecord[] reducedRecords)
        {
            using (AircraftMongoDbDal dal = new AircraftMongoDbDal())
            {
                MongoServer mongoServer = dal.GetMongoServer();
                //不用判断是否为空，必须不能为空才能继续，否则内部要抛异常
                try
                {//此方法操作的记录为跟架次密切相关，需要拆分存储的记录，最好在DAL里面去处理表名构建逻辑
                    MongoDatabase database = dal.GetMongoDatabaseByAircraftModel(mongoServer, flight.Aircraft.AircraftModel);
                    if (database != null)
                    {
                        MongoCollection<FlightDataEntities.Level1FlightRecord> modelCollection1
                            = dal.GetLevel1FlightRecordMongoCollectionByFlight(database, flight);
                        modelCollection1.InsertBatch(reducedRecords);

                        //MongoCollection<FlightDataEntities.Level2FlightRecord> modelCollection2
                        //    = dal.GetLevel2FlightRecordMongoCollectionByFlight(database, flight);
                        //modelCollection2.InsertBatch(level2Records);
                    }
                }
                catch (Exception e)
                {
                    LogHelper.Error("AddOneParameterValue", e);
                    return e.Message;
                }
            }
            return string.Empty;
        }

        internal string AddLevelTopFlightRecords(Flight flight, LevelTopFlightRecord[] topRecords)
        {
            using (AircraftMongoDbDal dal = new AircraftMongoDbDal())
            {
                MongoServer mongoServer = dal.GetMongoServer();
                //不用判断是否为空，必须不能为空才能继续，否则内部要抛异常
                try
                {//此方法操作的记录为跟架次密切相关，但肯定LevelTopRecord需要包含趋势分析等信息，
                    //建议不要分表，存放在Common里面
                    MongoDatabase database = dal.GetMongoDatabaseByAircraftModel(mongoServer, flight.Aircraft.AircraftModel);
                    if (database != null)
                    {
                        MongoCollection<FlightDataEntities.LevelTopFlightRecord> modelCollection1
                            = dal.GetLevelTopFlightRecordMongoCollectionByFlight(database, flight);
                        modelCollection1.InsertBatch(topRecords);

                        //MongoCollection<FlightDataEntities.Level2FlightRecord> modelCollection2
                        //    = dal.GetLevel2FlightRecordMongoCollectionByFlight(database, flight);
                        //modelCollection2.InsertBatch(level2Records);
                    }
                }
                catch (Exception e)
                {
                    LogHelper.Error("AddOneParameterValue", e);
                    return e.Message;
                }
            }
            return string.Empty;
        }
    }
}