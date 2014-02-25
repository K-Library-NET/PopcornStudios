using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftDataAnalysisWinRT.Services
{
    public class RTConverter
    {
        internal static AircraftDataInput.Flight ToDataInput(FlightDataEntitiesRT.Flight flight)
        {
            AircraftDataInput.Flight rt = new AircraftDataInput.Flight()
            {
                Aircraft = new AircraftDataInput.AircraftInstance()
                {
                    AircraftModel = new AircraftDataInput.AircraftModel()
                    {
                        ModelName = flight.Aircraft.AircraftModel.ModelName,
                        Caption = flight.Aircraft.AircraftModel.Caption
                    },
                    AircraftNumber = flight.Aircraft.AircraftNumber,
                    LastUsed = flight.Aircraft.LastUsed
                },
                FlightID = flight.FlightID,
                FlightName = flight.FlightName,
                StartSecond = flight.StartSecond,
                EndSecond = flight.EndSecond
            };

            return rt;
        }

        internal static FlightDataEntitiesRT.Flight FromDataInput(AircraftDataInput.Flight flight)
        {
            var rt = new FlightDataEntitiesRT.Flight()
            {
                Aircraft = new FlightDataEntitiesRT.AircraftInstance()
                {
                    AircraftModel = new FlightDataEntitiesRT.AircraftModel()
                    {
                        ModelName = flight.Aircraft.AircraftModel.ModelName,
                        Caption = flight.Aircraft.AircraftModel.Caption
                    },
                    AircraftNumber = flight.Aircraft.AircraftNumber,
                    LastUsed = flight.Aircraft.LastUsed
                },
                FlightID = flight.FlightID,
                FlightName = flight.FlightName,
                StartSecond = flight.StartSecond,
                EndSecond = flight.EndSecond
            };

            return rt;
        }

        internal static System.Collections.ObjectModel.ObservableCollection<AircraftDataInput.DecisionRecord>
            ToDataInput(List<FlightDataEntitiesRT.Decisions.DecisionRecord> decisionRecords)
        {
            if (decisionRecords == null || decisionRecords.Count <= 0)
                return new ObservableCollection<AircraftDataInput.DecisionRecord>();

            var recs = from one in decisionRecords
                       select new AircraftDataInput.DecisionRecord()
                       {
                           DecisionID = one.DecisionID,
                           EventLevel = one.EventLevel,
                           DecisionName = one.DecisionName,
                           DecisionDescription = one.DecisionDescription,
                           EndSecond = one.EndSecond,
                           FlightID = one.FlightID,
                           HappenSecond = one.HappenSecond,
                           StartSecond = one.StartSecond
                       };

            System.Collections.ObjectModel.ObservableCollection<AircraftDataInput.DecisionRecord> result =
                new System.Collections.ObjectModel.ObservableCollection<AircraftDataInput.DecisionRecord>(recs);

            return result;
        }

        internal static AircraftDataInput.Level2FlightRecord ToDataInput(FlightDataEntitiesRT.Level2FlightRecord level2Record)
        {
            return new AircraftDataInput.Level2FlightRecord()
            {
                EndSecond = level2Record.EndSecond,
                FlightID = level2Record.FlightID,
                ParameterID = level2Record.FlightID,
                StartSecond = level2Record.StartSecond,
                ExtremumPointInfo = RTConverter.ToDataInput(level2Record.ExtremumPointInfo)
            };
        }

        internal static System.Collections.ObjectModel.ObservableCollection<AircraftDataInput.Level1FlightRecord>
            ToDataInput(FlightDataEntitiesRT.Level1FlightRecord[] reducedRecords)
        {
            System.Collections.ObjectModel.ObservableCollection<AircraftDataInput.Level1FlightRecord> records
                = new System.Collections.ObjectModel.ObservableCollection<AircraftDataInput.Level1FlightRecord>();

            foreach (var red in reducedRecords)
            {
                records.Add(new AircraftDataInput.Level1FlightRecord()
                {
                    FlightID = red.FlightID,
                    ParameterID = red.ParameterID,
                    StartSecond = red.StartSecond,
                    EndSecond = red.EndSecond,
                    Values = new System.Collections.ObjectModel.ObservableCollection<float>(red.Values)
                });
            }

            return records;
        }

        internal static FlightDataEntitiesRT.AircraftModel FromDataInput(AircraftService.AircraftModel aircraftModel)
        {
            return new FlightDataEntitiesRT.AircraftModel()
            {
                Caption = aircraftModel.Caption,
                LastUsed = aircraftModel.LastUsed,
                ModelName = aircraftModel.ModelName
            };
        }

        internal static AircraftService.Flight ToAircraftService(FlightDataEntitiesRT.Flight flight)
        {
            return new AircraftService.Flight()
            {
                EndSecond = flight.EndSecond,
                StartSecond = flight.StartSecond,
                FlightID = flight.FlightID,
                FlightName = flight.FlightName,
                Aircraft = new AircraftService.AircraftInstance()
                {
                    AircraftModel = new AircraftService.AircraftModel()
                    {
                        Caption = flight.Aircraft.AircraftModel.Caption,
                        LastUsed = flight.Aircraft.AircraftModel.LastUsed,
                        ModelName = flight.Aircraft.AircraftModel.ModelName
                    }
                    ,
                    AircraftNumber = flight.Aircraft.AircraftNumber,
                    LastUsed = flight.Aircraft.LastUsed
                },
            };
        }

        internal static AircraftService.AircraftModel ToAircraftService(FlightDataEntitiesRT.AircraftModel aircraftModel)
        {
            return new AircraftService.AircraftModel()
            {
                ModelName = aircraftModel.ModelName,
                LastUsed = aircraftModel.LastUsed,
                Caption = aircraftModel.Caption
            };
        }

        internal static FlightDataEntitiesRT.FlightParameters FromAircraftService(AircraftService.FlightParameters parameters)
        {
            if (parameters == null)
                return null;

            return new FlightDataEntitiesRT.FlightParameters()
            {
                BytesCount = parameters.BytesCount,
                Parameters = parameters.Parameters.Select<AircraftService.FlightParameter, FlightDataEntitiesRT.FlightParameter>(
                new Func<AircraftService.FlightParameter, FlightDataEntitiesRT.FlightParameter>(
                    delegate(AircraftService.FlightParameter par)
                    {
                        return new FlightDataEntitiesRT.FlightParameter()
                        {
                            Caption = par.Caption,
                            Index = par.Index,
                            SubIndex = par.SubIndex,
                            ModelName = par.ModelName,
                            ParameterDataType = par.ParameterDataType,
                            ParameterID = par.ParameterID,
                            Unit = par.Unit,
                            ByteIndexes = par.ByteIndexes == null ? new FlightDataEntitiesRT.ByteIndex[] { } :
                            (from bi in par.ByteIndexes
                             select new FlightDataEntitiesRT.ByteIndex()
                             {
                                 Index = bi.Index,
                                 SubIndexes = bi.SubIndexes == null ? new FlightDataEntitiesRT.BitIndex[] { } :
                                 (from si in bi.SubIndexes
                                  select new FlightDataEntitiesRT.BitIndex() { SubIndex = si.SubIndex }
                                 ).ToArray()
                             }).ToArray()
                        };
                    })).ToArray()
            };
        }

        internal static FlightDataEntitiesRT.Flight FromAircraftService(AircraftService.Flight flight)
        {
            return new FlightDataEntitiesRT.Flight()
            {
                Aircraft = new FlightDataEntitiesRT.AircraftInstance()
                {
                    AircraftModel = FromAircraftService(flight.Aircraft.AircraftModel),
                    AircraftNumber = flight.Aircraft.AircraftNumber,
                    LastUsed = flight.Aircraft.LastUsed
                },
                FlightName = flight.FlightName,
                FlightID = flight.FlightID,
                StartSecond = flight.StartSecond,
                EndSecond = flight.EndSecond
            };
        }

        internal static FlightDataEntitiesRT.AircraftModel FromAircraftService(AircraftService.AircraftModel aircraftModel)
        {
            return new FlightDataEntitiesRT.AircraftModel()
            {
                Caption = aircraftModel.Caption,
                LastUsed = aircraftModel.LastUsed,
                ModelName = aircraftModel.ModelName
            };
        }

        internal static FlightDataEntitiesRT.Decisions.Decision FromAircraftService(AircraftService.Decision one)
        {
            var decisions = new FlightDataEntitiesRT.Decisions.Decision()
            {
                DecisionID = one.DecisionID,
                DecisionName = one.DecisionName,
                EventLevel = one.EventLevel,
                LastTime = one.LastTime,
                RelatedParameters = one.RelatedParameters.ToArray(),
                DecisionDescriptionStringTemplate = one.DecisionDescriptionStringTemplate,
                Conditions =
                (from two in one.Conditions
                 select RTConverter.FromAircraftService(two)).ToArray()
            };
            return decisions;
        }

        internal static FlightDataEntitiesRT.Decisions.FlightConditionDecision FromAircraftService(AircraftService.FlightConditionDecision one)
        {
            var decisions = new FlightDataEntitiesRT.Decisions.FlightConditionDecision()
            {
                DecisionID = one.DecisionID,
                DecisionName = one.DecisionName,
                EventLevel = one.EventLevel,
                LastTime = one.LastTime,
                RelatedParameters = one.RelatedParameters.ToArray(),
                DecisionDescriptionStringTemplate = one.DecisionDescriptionStringTemplate,
                Conditions =
                (from two in one.Conditions
                 select RTConverter.FromAircraftService(two)).ToArray()
            };
            return decisions;
        }

        public static FlightDataEntitiesRT.Decisions.SubCondition FromAircraftService(AircraftService.SubCondition two)
        {
            if (two.ConditionType == AircraftService.SubConditionType.DeltaRate)
            {
                return new FlightDataEntitiesRT.Decisions.DeltaRateSubCondition()
                {
                    Operator = FromAircraftService(two.Operator),
                    ParameterID = two.ParameterID,
                    ParameterValue = two.ParameterValue,
                    Relation = FromAircraftService(two.Relation),
                    SubConditions = two.SubConditions == null ? null :
                                    (from three in two.SubConditions
                                     select RTConverter.FromAircraftService(three)).ToArray()
                };
            }
            else
            {
                return new FlightDataEntitiesRT.Decisions.CompareSubCondition()
                {
                    Operator = FromAircraftService(two.Operator),
                    ParameterID = two.ParameterID,
                    ParameterValue = two.ParameterValue,
                    Relation = FromAircraftService(two.Relation),
                    SubConditions = two.SubConditions == null ? null :
                                    (from three in two.SubConditions
                                     select RTConverter.FromAircraftService(three)).ToArray()
                };
            }
        }

        public static FlightDataEntitiesRT.Decisions.ConditionRelation FromAircraftService(
            AircraftService.ConditionRelation conditionRelation)
        {
            switch (conditionRelation)
            {
                case AircraftService.ConditionRelation.OR: return FlightDataEntitiesRT.Decisions.ConditionRelation.OR;
                default: return FlightDataEntitiesRT.Decisions.ConditionRelation.AND;
            }
        }

        internal static FlightDataEntitiesRT.Decisions.CompareOperator FromAircraftService(
            AircraftService.CompareOperator compareOperator)
        {
            switch (compareOperator)
            {
                case AircraftService.CompareOperator.GreaterOrEqual: return FlightDataEntitiesRT.Decisions.CompareOperator.GreaterOrEqual;
                case AircraftService.CompareOperator.GreaterThan: return FlightDataEntitiesRT.Decisions.CompareOperator.GreaterThan;
                case AircraftService.CompareOperator.NotEqual: return FlightDataEntitiesRT.Decisions.CompareOperator.NotEqual;
                case AircraftService.CompareOperator.SmallerOrEqual: return FlightDataEntitiesRT.Decisions.CompareOperator.SmallerOrEqual;
                case AircraftService.CompareOperator.SmallerThan: return FlightDataEntitiesRT.Decisions.CompareOperator.SmallerThan;
                default: return FlightDataEntitiesRT.Decisions.CompareOperator.Equal;
            }
        }

        internal static FlightDataEntitiesRT.Charts.ChartPanel FromAircraftService(AircraftService.ChartPanel one)
        {
            if (one == null)
                return null;
            return new FlightDataEntitiesRT.Charts.ChartPanel()
            {
                PanelID = one.PanelID,
                PanelName = one.PanelName,
                ParameterIDs = one.ParameterIDs.ToArray()
            };
        }

        internal static System.Collections.ObjectModel.ObservableCollection<AircraftDataInput.Level2FlightRecord>
            ToDataInput(FlightDataEntitiesRT.Level2FlightRecord[] level2Records)
        {
            System.Collections.ObjectModel.ObservableCollection<AircraftDataInput.Level2FlightRecord> recs = null;
            // new System.Collections.ObjectModel.ObservableCollection<AircraftDataInput.Level2FlightRecord>();

            var result = from one in level2Records
                         select RTConverter.ToDataInput(one);
            recs = new System.Collections.ObjectModel.ObservableCollection<AircraftDataInput.Level2FlightRecord>(result);

            return recs;
        }

        internal static FlightDataEntitiesRT.ExtremumPointInfo FromAircraftService(AircraftService.ExtremumPointInfo one)
        {
            if (one == null)
                return null;

            FlightDataEntitiesRT.ExtremumPointInfo info = new FlightDataEntitiesRT.ExtremumPointInfo()
            {
                MaxValue = one.MaxValue,
                MaxValueSecond = one.MaxValueSecond,
                MinValue = one.MinValue,
                MinValueSecond = one.MinValueSecond,
                ParameterID = one.ParameterID
            };

            return info;
        }

        internal static FlightDataEntitiesRT.LevelTopFlightRecord FromAircraftService(AircraftService.LevelTopFlightRecord one)
        {
            FlightDataEntitiesRT.LevelTopFlightRecord rec = new FlightDataEntitiesRT.LevelTopFlightRecord()
            {
                EndSecond = one.EndSecond,
                FlightID = one.FlightID,
                ParameterID = one.ParameterID,
                StartSecond = one.StartSecond,
                ExtremumPointInfo = FromAircraftService(one.ExtremumPointInfo),
                Level2FlightRecord = FromAircraftService(one.Level2FlightRecord)
            };

            return rec;
        }

        internal static FlightDataEntitiesRT.Level2FlightRecord[] FromAircraftService(
            System.Collections.ObjectModel.ObservableCollection<AircraftService.Level2FlightRecord> observableCollection)
        {
            var result = from one in observableCollection
                         select new FlightDataEntitiesRT.Level2FlightRecord()
                         {
                             EndSecond = one.EndSecond,
                             FlightID = one.FlightID,
                             ParameterID = one.ParameterID,
                             StartSecond = one.StartSecond,
                             ExtremumPointInfo = FromAircraftService(one.ExtremumPointInfo)
                         };

            return result.ToArray();
        }

        internal static AircraftDataInput.LevelTopFlightRecord ToDataInput(FlightDataEntitiesRT.LevelTopFlightRecord one)
        {
            return new AircraftDataInput.LevelTopFlightRecord()
            {
                ExtremumPointInfo = RTConverter.ToDataInput(one.ExtremumPointInfo),
                EndSecond = one.EndSecond,
                FlightID = one.FlightID,
                Level2FlightRecord = ToDataInput(one.Level2FlightRecord),
                ParameterID = one.ParameterID,
                StartSecond = one.StartSecond
            };
        }

        internal static AircraftDataInput.ExtremumPointInfo ToDataInput(
            FlightDataEntitiesRT.ExtremumPointInfo extremumPointInfo)
        {
            return new AircraftDataInput.ExtremumPointInfo()
            {
                FlightID = extremumPointInfo.FlightID,
                MaxValue = extremumPointInfo.MaxValue,
                MaxValueSecond = extremumPointInfo.MaxValueSecond,
                MinValue = extremumPointInfo.MinValue,
                MinValueSecond = extremumPointInfo.MinValueSecond,
                ParameterID = extremumPointInfo.ParameterID
            };
        }

        internal static FlightDataEntitiesRT.Decisions.DecisionRecord FromAircraftService(
            AircraftService.DecisionRecord one)
        {
            return new FlightDataEntitiesRT.Decisions.DecisionRecord()
            {
                DecisionID = one.DecisionID,
                FlightID = one.FlightID,
                EventLevel = one.EventLevel,
                DecisionDescription = one.DecisionDescription,
                DecisionName = one.DecisionName,
                StartSecond = one.StartSecond,
                EndSecond = one.EndSecond,
                HappenSecond = one.HappenSecond
            };
        }

        internal static ObservableCollection<KeyValuePair<string, ObservableCollection<FlightDataEntitiesRT.ParameterRawData>>>
            FromAircraftService(ObservableCollection<KeyValuePair<string, ObservableCollection<AircraftService.FlightRawData>>> observableCollection)
        {
            var result = from one in observableCollection
                         select new KeyValuePair<string, ObservableCollection<FlightDataEntitiesRT.ParameterRawData>>(
                             one.Key, RTConverter.FromAircraftService(one.Value));

            return new ObservableCollection<KeyValuePair<string,
                ObservableCollection<FlightDataEntitiesRT.ParameterRawData>>>(result);
        }

        internal static ObservableCollection<FlightDataEntitiesRT.ParameterRawData>
            FromAircraftService(ObservableCollection<AircraftService.FlightRawData> observableCollection)
        {
            var result = from one in observableCollection
                         select new FlightDataEntitiesRT.ParameterRawData()
                         {
                             ParameterID = one.ParameterID,
                             Second = one.Second,
                             Values = one.Values.ToArray()
                         };

            if (result == null || result.Count() <= 0)
                return new ObservableCollection<FlightDataEntitiesRT.ParameterRawData>();

            return new ObservableCollection<FlightDataEntitiesRT.ParameterRawData>(result);
        }

        internal static AircraftDataAnalysisWinRT.AircraftDataInput.FlightRawDataRelationPoint
            ToDataInput(FlightDataEntitiesRT.FlightRawDataRelationPoint one)
        {
            return new AircraftDataInput.FlightRawDataRelationPoint()
            {
                FlightDate = one.FlightDate,
                FlightID = one.FlightID,
                XAxisParameterID = one.XAxisParameterID,
                XAxisParameterValue = one.XAxisParameterValue,
                YAxisParameterID = one.YAxisParameterID,
                YAxisParameterValue = one.YAxisParameterValue
            };
        }

        internal static AircraftService.AircraftInstance ToAircraftService(FlightDataEntitiesRT.AircraftInstance aircraftInstance)
        {
            return new AircraftService.AircraftInstance()
                   {
                       AircraftModel = new AircraftService.AircraftModel()
                       {
                           Caption = aircraftInstance.AircraftModel.Caption,
                           LastUsed = aircraftInstance.AircraftModel.LastUsed,
                           ModelName = aircraftInstance.AircraftModel.ModelName
                       }
                       ,
                       AircraftNumber = aircraftInstance.AircraftNumber,
                       LastUsed = aircraftInstance.LastUsed
                   };
        }

        internal static FlightDataEntitiesRT.FlightRawDataRelationPoint FromAircraftService(AircraftService.FlightRawDataRelationPoint one)
        {
            return new FlightDataEntitiesRT.FlightRawDataRelationPoint()
            {
                FlightDate = one.FlightDate,
                FlightID = one.FlightID,
                XAxisParameterID = one.XAxisParameterID,
                XAxisParameterValue = one.XAxisParameterValue,
                YAxisParameterID = one.YAxisParameterID,
                YAxisParameterValue = one.YAxisParameterValue
            };
        }
    }
}
