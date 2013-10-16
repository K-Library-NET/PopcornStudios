using FlightDataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using System.Configuration;
using System.Xml.Linq;
using System.IO;

namespace AircraftDataAnalysisWcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“AircraftService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 AircraftService.svc 或 AircraftService.svc.cs，然后开始调试。
    public class AircraftService : IAircraftService
    {
        public FlightDataEntities.Charts.ChartPanel[] GetAllChartPanels(AircraftModel aircraftModel)
        {
            throw new NotImplementedException();
        }

        public KeyValuePair<string, FlightDataEntities.FlightRawData[]>[] GetFlightData(Flight flight,
            string[] parameterIds, int startSecond, int endSecond)
        {
            throw new NotImplementedException();
        }

        public FlightDataEntities.LevelTopFlightRecord[] GetLevelTopFlightRecords(
            Flight flight, string[] parameterIds, bool withLevel1Data)
        {
            throw new NotImplementedException();
        }

        public string AddOrUpdateDecisionRecords(
            Flight flight, FlightDataEntities.DecisionRecord[] records)
        {
            throw new NotImplementedException();
        }

        private string m_mongoConnectionString = "mongodb://localhost/?w=1";

        [Obsolete]
        public string[] GetAllAircraftModelNames()
        {

            string[] dbnames = null;
            MongoServer mongoServer = this.GetMongoServer();

            Exception ex = null;

            try
            {
                mongoServer.Connect();
                dbnames = mongoServer.GetDatabaseNames().ToArray();
            }
            catch (Exception e)
            {
                ex = e;
            }
            finally
            {
                if (mongoServer != null)
                    mongoServer.Disconnect();
            }

            if (ex != null)
                throw ex;
            return dbnames;
        }

        private MongoServer GetMongoServer()
        {
            var mongoUrl = new MongoUrl(this.GetMongoConnectionString());
            var clientSettings = MongoClientSettings.FromUrl(mongoUrl);
            if (!clientSettings.WriteConcern.Enabled)
            {
                clientSettings.WriteConcern.W = 1; // ensure WriteConcern is enabled regardless of what the URL says
            }
            var mongoClient = new MongoClient(clientSettings);
            return mongoClient.GetServer();
        }

        private string GetMongoConnectionString()
        {
            return this.m_mongoConnectionString;
        }

        public FlightDataEntities.AircraftModel[] GetAllAircraftModels()
        {
            MongoServer mongoServer = this.GetMongoServer();
            if (mongoServer != null)
            {
                MongoDatabase database = mongoServer.GetDatabase(AircraftMongoDb.DATABASE_COMMON);
                if (database != null)
                {
                    MongoCollection<AircraftModel> modelCollection
                        = database.GetCollection<AircraftModel>(AircraftMongoDb.COLLECTION_AIRCRAFT_MODEL);

                    IQueryable<AircraftModel> models = modelCollection.AsQueryable<AircraftModel>();
                    var result = from one in models
                                 orderby one.LastUsed descending
                                 select one;

                    return result.ToArray();
                }
            }

            throw new Exception(string.Format(
                "No MongoServer {0} finded, or no MongoCollection {1} finded.",
                AircraftMongoDb.DATABASE_COMMON, AircraftMongoDb.COLLECTION_AIRCRAFT_MODEL));
        }

        public string AddOrUpdateAircraftModel(FlightDataEntities.AircraftModel aircraftModel)
        {
            if (aircraftModel == null)
                return "没有机型。";

            MongoServer mongoServer = this.GetMongoServer();
            if (mongoServer != null)
            {
                MongoDatabase database = mongoServer.GetDatabase(AircraftMongoDb.DATABASE_COMMON);
                if (database != null)
                {
                    MongoCollection<AircraftModel> modelCollection
                        = database.GetCollection<AircraftModel>(AircraftMongoDb.COLLECTION_AIRCRAFT_MODEL);

                    IQueryable<AircraftModel> models = modelCollection.AsQueryable<AircraftModel>();
                    var result = from one in models
                                 where one.ModelName == aircraftModel.ModelName
                                 select one;

                    if (result != null && result.Count() > 0)
                    {
                        foreach (var oneModel in result)
                        {//所有Property复制
                            oneModel.LastUsed = aircraftModel.LastUsed;
                            oneModel.Caption = aircraftModel.Caption;
                            modelCollection.Save(oneModel);
                        }
                    }
                    else
                    {
                        modelCollection.Insert(aircraftModel);
                    }

                    return string.Empty;
                }
            }

            return string.Format(
                "No MongoServer {0} finded, or no MongoCollection {1} finded.",
                AircraftMongoDb.DATABASE_COMMON, AircraftMongoDb.COLLECTION_AIRCRAFT_MODEL);
        }

        public string DeleteAircraft(string aircraftModel)
        {
            if (aircraftModel == null)
                return "没有机型。";

            MongoServer mongoServer = this.GetMongoServer();
            if (mongoServer != null)
            {
                MongoDatabase database = mongoServer.GetDatabase(AircraftMongoDb.DATABASE_COMMON);
                if (database != null)
                {
                    MongoCollection<AircraftModel> modelCollection
                        = database.GetCollection<AircraftModel>(AircraftMongoDb.COLLECTION_AIRCRAFT_MODEL);

                    IQueryable<AircraftModel> models = modelCollection.AsQueryable<AircraftModel>();
                    //var result = from one in models
                    //             where one.ModelName == aircraftModel//aircraftModel.ModelName
                    //             select one;
                    MongoDB.Driver.MongoCursor<AircraftModel> cursor = modelCollection.Find(
                          Query.EQ("ModelName", aircraftModel));
                    AircraftModel model = cursor.First();

                    modelCollection.Remove(
                        Query.EQ("ModelName", aircraftModel));

                    return string.Empty;
                }
            }

            return string.Format(
                "No MongoServer {0} finded, or no MongoCollection {1} finded.",
                AircraftMongoDb.DATABASE_COMMON, AircraftMongoDb.COLLECTION_AIRCRAFT_MODEL);
        }

        public string AddOrUpdateAircraftInstance(AircraftInstance aircraftInstance)
        {
            if (aircraftInstance == null)
                return "没有机号。";

            MongoServer mongoServer = this.GetMongoServer();
            if (mongoServer != null)
            {
                MongoDatabase database = mongoServer.GetDatabase(AircraftMongoDb.DATABASE_COMMON);
                if (database != null)
                {
                    MongoCollection<AircraftInstance> modelCollection
                        = database.GetCollection<AircraftInstance>(AircraftMongoDb.COLLECTION_AIRCRAFT_INSTANCE);

                    IQueryable<AircraftInstance> models = modelCollection.AsQueryable<AircraftInstance>();
                    var result = from one in models
                                 where one.AircraftNumber == aircraftInstance.AircraftNumber
                                    && one.AircraftModel.ModelName == aircraftInstance.AircraftModel.ModelName
                                 select one;

                    if (result != null && result.Count() > 0)
                    {
                        foreach (var oneModel in result)
                        {//所有Property复制
                            oneModel.LastUsed = aircraftInstance.LastUsed;
                            oneModel.AircraftModel = aircraftInstance.AircraftModel;
                            modelCollection.Save(oneModel);
                        }
                    }
                    else
                    {
                        modelCollection.Insert(aircraftInstance);
                    }

                    return string.Empty;
                }
            }

            return string.Format(
                "No MongoServer {0} finded, or no MongoCollection {1} finded.",
                AircraftMongoDb.DATABASE_COMMON, AircraftMongoDb.COLLECTION_AIRCRAFT_INSTANCE);
        }

        public AircraftInstance[] GetAllAircraftInstances()
        {
            return GetAllAircraftInstances(string.Empty);
        }

        public AircraftInstance[] GetAllAircraftInstances(string modelName)
        {
            MongoServer mongoServer = this.GetMongoServer();
            if (mongoServer != null)
            {
                MongoDatabase database = mongoServer.GetDatabase(AircraftMongoDb.DATABASE_COMMON);
                if (database != null)
                {
                    MongoCollection<AircraftInstance> modelCollection
                        = database.GetCollection<AircraftInstance>(
                        AircraftMongoDb.COLLECTION_AIRCRAFT_INSTANCE);

                    IQueryable<AircraftInstance> models = modelCollection.AsQueryable<AircraftInstance>();

                    if (string.IsNullOrEmpty(modelName))
                        return models.ToArray();

                    var results = from one in models
                                  where one.AircraftModel != null && one.AircraftModel.ModelName == modelName
                                  select one;

                    if (results != null && results.Count() > 0)
                        return results.ToArray();

                    return new AircraftInstance[] { };
                }
            }

            throw new Exception(string.Format(
                "No MongoServer {0} finded, or no MongoCollection {1} finded.",
                AircraftMongoDb.DATABASE_COMMON, AircraftMongoDb.COLLECTION_AIRCRAFT_INSTANCE));
        }

        public AircraftInstance GetAircraftInstance(string aircraftNumber, string modelName)
        {
            MongoServer mongoServer = this.GetMongoServer();
            if (mongoServer != null)
            {
                MongoDatabase database = mongoServer.GetDatabase(AircraftMongoDb.DATABASE_COMMON);
                if (database != null)
                {
                    MongoCollection<AircraftInstance> modelCollection
                        = database.GetCollection<AircraftInstance>(
                        AircraftMongoDb.COLLECTION_AIRCRAFT_INSTANCE);

                    IQueryable<AircraftInstance> models
                        = modelCollection.AsQueryable<AircraftInstance>();

                    if (string.IsNullOrEmpty(aircraftNumber))
                        return models.First();

                    if (string.IsNullOrEmpty(modelName))
                    {
                        var results = from one in models
                                      where one.AircraftNumber == aircraftNumber
                                      select one;

                        if (results != null && results.Count() > 0)
                            return results.First();
                    }
                    else
                    {
                        var results = from one in models
                                      where one.AircraftNumber == aircraftNumber
                                      && one.AircraftModel != null && one.AircraftModel.ModelName == modelName
                                      select one;

                        if (results != null && results.Count() > 0)
                            return results.First();
                    }

                    return null;
                }
            }

            throw new Exception(string.Format(
                "No MongoServer {0} finded, or no MongoCollection {1} finded.",
                AircraftMongoDb.DATABASE_COMMON, AircraftMongoDb.COLLECTION_AIRCRAFT_INSTANCE));
        }

        public string AddOrUpdateFlyParameter(FlightParameter flightParameter)
        {
            return this.AddOrUpdateFlyParameter(new FlightParameter[] { flightParameter });
        }

        public string AddOrUpdateFlyParameter(FlightParameter[] flightParameter)
        {
            MongoServer mongoServer = this.GetMongoServer();
            if (mongoServer != null)
            {
                MongoDatabase database = mongoServer.GetDatabase(AircraftMongoDb.DATABASE_COMMON);
                if (database != null)
                {
                    MongoCollection<FlightParameter> modelCollection
                        = database.GetCollection<FlightParameter>(
                        AircraftMongoDb.COLLECTION_FLIGHT_PARAMETER);

                    foreach (var fp in flightParameter)
                    {
                        MongoCursor<FlightParameter> pms =
                         modelCollection.Find(Query.And(Query.EQ("ParameterID", fp.ParameterID),
                            Query.EQ("ModelName", fp.ModelName)));
                        if (pms != null && pms.Count() > 0)
                        {
                            foreach (var pm in pms)
                            {
                                pm.IsConcerned = fp.IsConcerned;
                                pm.Caption = fp.Caption;
                                pm.Index = fp.Index;
                                pm.SubIndex = fp.SubIndex;
                                pm.Unit = fp.Unit;
                            }
                        }
                        else
                        {
                            modelCollection.Insert(fp);
                        }
                    }

                    return string.Empty;
                }
            }

            return string.Format(
                "No MongoServer {0} finded, or no MongoCollection {1} finded.",
                AircraftMongoDb.DATABASE_COMMON, AircraftMongoDb.COLLECTION_FLIGHT_PARAMETER);
        }

        public FlightParameters GetAllFlightParameters(AircraftModel model)
        {
            string basePath = this.CombineFromBasePath("FlightParameters.xml");

            if (!string.IsNullOrEmpty(basePath) && File.Exists(basePath))
                using (StreamReader reader = new StreamReader(basePath))
                {
                    XElement element = XElement.Load(reader);
                    if (element != null)
                    {
                        FlightParameters root = new FlightParameters();
                        var bc = element.Attribute("BytesCount");
                        if (bc != null)
                        {
                            root.BytesCount = Convert.ToInt32(bc.Value);
                        }

                        var parameters = element.Descendants("Parameter");
                        if (parameters != null && parameters.Count() > 0)
                        {
                            try
                            {
                                List<FlightParameter> fps = new List<FlightParameter>();

                                foreach (var one in parameters)
                                {
                                    FlightParameter ps = new FlightParameter()
                                             {
                                                 ParameterID = one.Attribute("ParameterID").Value,
                                                 Caption = one.Attribute("Caption").Value,
                                                 Index = Convert.ToInt32(one.Attribute("Index").Value),
                                                 SubIndex = Convert.ToInt32(one.Attribute("SubIndex").Value),
                                                 ParameterDataType = one.Attribute("ParameterDataType").Value
                                             };
                                    var eles = one.Elements("Byte");
                                    if (eles != null && eles.Count() > 0)
                                    {
                                        List<ByteIndex> bis = new List<ByteIndex>();
                                        foreach (var ele in eles)
                                        {
                                            ByteIndex bi = new ByteIndex() { Index = Convert.ToInt32(ele.Attribute("Index").Value) };

                                            var eles2 = ele.Elements("Bit");
                                            if (eles2 != null && eles2.Count() > 0)
                                            {
                                                List<BitIndex> bitIndex = new List<BitIndex>();
                                                foreach (var ele2 in eles2)
                                                {
                                                    bitIndex.Add(
                                                        new BitIndex() { SubIndex = Convert.ToInt32(ele2.Attribute("Index").Value) });
                                                }
                                                bi.SubIndexes = bitIndex.ToArray();
                                            }

                                            bis.Add(bi);
                                        }

                                        ps.ByteIndexes = bis.ToArray();
                                    }

                                    fps.Add(ps);
                                }

                                root.Parameters = fps.ToArray();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }

                        return root;
                    }
                }

            return null;//this.GetAllFlightParameters(this.GetCurrentAircraftModel().ModelName);
        }

        private string CombineFromBasePath(string path)
        {
            string path2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
            if (File.Exists(path2) || Directory.Exists(path2))
                return path2;

            string path1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");

            return Path.Combine(path1, path);
        }

        public FlightParameter[] GetAllFlightParameters(string modelName)
        {
            MongoServer mongoServer = this.GetMongoServer();
            if (mongoServer != null)
            {
                MongoDatabase database = mongoServer.GetDatabase(AircraftMongoDb.DATABASE_COMMON);
                if (database != null)
                {
                    MongoCollection<FlightParameter> modelCollection
                        = database.GetCollection<FlightParameter>(
                        AircraftMongoDb.COLLECTION_FLIGHT_PARAMETER);

                    IQueryable<FlightParameter> models = modelCollection.AsQueryable<FlightParameter>();

                    var results = from one in models
                                  where one.ModelName == modelName
                                  orderby one.Index, one.SubIndex
                                  select one;

                    if (results != null && results.Count() > 0)
                        return results.ToArray();

                    return new FlightParameter[] { };
                }
            }

            throw new Exception(string.Format(
                "No MongoServer {0} finded, or no MongoCollection {1} finded.",
                AircraftMongoDb.DATABASE_COMMON, AircraftMongoDb.COLLECTION_FLIGHT_PARAMETER));
        }

        public string InsertRawDataBatch(Flight flight, Level1FlightRecord[] batchData)
        {
            if (!this.IsValidAircraftInfo(flight))
                return "缺少机型或架次信息。";
            if (batchData == null || batchData.Length <= 0)
                return "无数据输入。";

            MongoServer mongoServer = this.GetMongoServer();
            if (mongoServer != null)
            {
                MongoDatabase database = mongoServer.GetDatabase(flight.Aircraft.AircraftModel.ModelName);
                if (database != null)
                {
                    MongoCollection<Level1FlightRecord> modelCollection
                        = database.GetCollection<Level1FlightRecord>(
                        AircraftMongoDb.COLLECTION_FLIGHT_RECORD_LEVEL1 +
                        flight.FlightID);
                    //数据精简由客户端完成

                    modelCollection.InsertBatch(batchData);
                }
            }

            return string.Empty;
        }

        //private bool IsValidAircraftInfo(RawDataBatch batchData)
        //{
        //    if (batchData == null)
        //        return false;

        //    return IsValidAircraftInfo(batchData.Flight);
        //}

        private bool IsValidAircraftInfo(Flight flight)
        {
            if (flight == null)
                return false;

            if (flight == null || string.IsNullOrEmpty(flight.FlightID)
                || flight.Aircraft == null || flight.Aircraft.AircraftModel == null ||
                string.IsNullOrEmpty(flight.Aircraft.AircraftModel.ModelName))
                return false;

            return true;
        }

        public string OptimizeForLevel2Data(Flight flight)
        {
            if (!this.IsValidAircraftInfo(flight))
                return "缺少机型或架次信息。";


            //产生汇总数据
            MongoServer mongoServer = this.GetMongoServer();
            if (mongoServer != null)
            {
                MongoDatabase database = mongoServer.GetDatabase(flight.Aircraft.AircraftModel.ModelName);
                if (database != null)
                {
                    MongoCollection<Level1FlightRecord> modelCollection
                        = database.GetCollection<Level1FlightRecord>(
                        AircraftMongoDb.COLLECTION_FLIGHT_RECORD_LEVEL1 +
                        flight.FlightID);

                    Level2FlightRecord[] records =
                        null;//DEBUG
                    //  FlightDataEntityTransform.FromLevel1RecordCollectionToLevel2Record(flight, modelCollection);
                }
            }

            return string.Empty;
        }

        private static AircraftModel m_currentAircraftModel = null;

        public AircraftModel GetCurrentAircraftModel()
        {
            if (m_currentAircraftModel == null)
                this.InitCurrentAircraftModel();

            return m_currentAircraftModel;
        }

        private void InitCurrentAircraftModel()
        {
            string modelName = ConfigurationManager.AppSettings["AircraftModelName"];
            string modelCaption = ConfigurationManager.AppSettings["AircraftModelCaption"];

            AircraftModel model = new AircraftModel() { ModelName = modelName, Caption = modelCaption, LastUsed = DateTime.Now };

            m_currentAircraftModel = model;
        }

    }
}
