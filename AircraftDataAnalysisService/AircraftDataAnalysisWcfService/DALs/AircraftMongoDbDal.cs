using FlightDataEntities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AircraftDataAnalysisWcfService.DALs
{
    /// <summary>
    /// 负责MongoDB Driver操作，主要实现using语句的释放。
    /// 还有记录要根据机型、架次分开存储的，需要DAL来屏蔽找Collection的逻辑
    /// 机型分割MongoDatabase，除了Common是用于直接取得实体对象（机型、架次、机号、面板、参数这些等）：
    /// </summary>
    public class AircraftMongoDbDal : IDisposable
    {
        private MongoServer m_mongoServer = null;

        public MongoServer GetMongoServer()
        {
            try
            {
                var mongoUrl = new MongoUrl(AircraftMongoDb.ConnectionString);
                var clientSettings = MongoClientSettings.FromUrl(mongoUrl);
                if (!clientSettings.WriteConcern.Enabled)
                {
                    clientSettings.WriteConcern.W = 1; // ensure WriteConcern is enabled regardless of what the URL says
                }
                var mongoClient = new MongoClient(clientSettings);
                this.m_mongoServer = mongoClient.GetServer();
                m_mongoServer.Connect();
                return m_mongoServer;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 释放和Disconnect，强行要吞掉异常
        /// </summary>
        public void Dispose()
        {
            try
            {
                if (m_mongoServer != null)
                {
                    m_mongoServer.Disconnect();
                    m_mongoServer = null;
                }
            }
            catch (Exception e)
            {
                LogHelper.Warn("释放MongoDB Server异常。", e);
            }
        }

        internal MongoCollection<FlightDataEntities.Decisions.DecisionRecord> GetDecisionRecordMongoCollectionByFlight(
            MongoDatabase database, Flight flight)
        {
            return database.GetCollection<FlightDataEntities.Decisions.DecisionRecord>(
                AircraftMongoDb.COLLECTION_FLIGHT_DECISIONS + "_" + flight.FlightID);
        }

        /// <summary>
        /// 传空则直接取得Common库
        /// </summary>
        /// <param name="mongoServer"></param>
        /// <param name="aircraftModel"></param>
        /// <returns></returns>
        internal MongoDatabase GetMongoDatabaseByAircraftModel(MongoServer mongoServer, AircraftModel aircraftModel)
        {
            if (aircraftModel != null && !string.IsNullOrEmpty(aircraftModel.ModelName))
            {
                return mongoServer.GetDatabase(AircraftMongoDb.COLLECTION_AIRCRAFT_INSTANCE + "_" + aircraftModel.ModelName);
            }

            return mongoServer.GetDatabase(AircraftMongoDb.DATABASE_COMMON);
        }

        /// <summary>
        /// 取Common库
        /// </summary>
        /// <param name="mongoServer"></param>
        /// <returns></returns>
        internal MongoDatabase GetMongoDatabaseCommon(MongoServer mongoServer)
        {
            return this.GetMongoDatabaseByAircraftModel(mongoServer, null);
        }

        internal MongoCollection<Level1FlightRecord> GetLevel1FlightRecordMongoCollectionByFlight(MongoDatabase database, Flight flight)
        {
            return database.GetCollection<Level1FlightRecord>(
                AircraftMongoDb.COLLECTION_FLIGHT_RECORD_LEVEL1 + "_" + flight.FlightID);
        }

        internal MongoCollection<Level2FlightRecord> GetLevel2FlightRecordMongoCollectionByFlight(MongoDatabase database, Flight flight)
        {
            return database.GetCollection<Level2FlightRecord>(
                AircraftMongoDb.COLLECTION_FLIGHT_RECORD_LEVELTOP + "_" + flight.FlightID);
        }

        internal MongoCollection<LevelTopFlightRecord> GetLevelTopFlightRecordMongoCollectionByFlight(MongoDatabase database, Flight flight)
        {
            return database.GetCollection<LevelTopFlightRecord>(
                AircraftMongoDb.COLLECTION_FLIGHT_RECORD_LEVELTOP);// + "_" + flight.FlightID);
        }
    }
}