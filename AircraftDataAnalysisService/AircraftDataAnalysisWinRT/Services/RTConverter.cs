using System;
using System.Collections.Generic;
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
            var recs = from one in decisionRecords
                       select new AircraftDataInput.DecisionRecord()
                       {
                           DecisionID = one.DecisionID,
                           DecisionName = one.DecisionName,
                           DecisionDescription = one.DecisionDescription,
                           EndSecond = one.EndSecond,
                           FlightID = one.FlightID,
                           StartSecond = one.StartSecond
                       };

            System.Collections.ObjectModel.ObservableCollection<AircraftDataInput.DecisionRecord> result =
                new System.Collections.ObjectModel.ObservableCollection<AircraftDataInput.DecisionRecord>(recs);

            return result;
        }

        internal static AircraftDataInput.Level2FlightRecord ToDataInput(FlightDataEntitiesRT.Level2FlightRecord level2Record)
        {
            throw new NotImplementedException();
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
                LastTime = one.LastTime,
                RelatedParameters = one.RelatedParameters.ToArray(),
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

        private static FlightDataEntitiesRT.Decisions.CompareOperator FromAircraftService(
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
    }
}
