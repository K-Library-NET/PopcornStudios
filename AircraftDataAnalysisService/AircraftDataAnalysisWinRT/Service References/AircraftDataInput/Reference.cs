﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18408
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This code was auto-generated by Microsoft.VisualStudio.ServiceReference.Platforms, version 11.0.50727.1
// 
namespace AircraftDataAnalysisWinRT.AircraftDataInput {
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Flight", Namespace="http://schemas.datacontract.org/2004/07/FlightDataEntities")]
    public partial class Flight : object, System.ComponentModel.INotifyPropertyChanged {
        
        private AircraftDataAnalysisWinRT.AircraftDataInput.AircraftInstance AircraftField;
        
        private int EndSecondField;
        
        private string FlightIDField;
        
        private string FlightNameField;
        
        private int StartSecondField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public AircraftDataAnalysisWinRT.AircraftDataInput.AircraftInstance Aircraft {
            get {
                return this.AircraftField;
            }
            set {
                if ((object.ReferenceEquals(this.AircraftField, value) != true)) {
                    this.AircraftField = value;
                    this.RaisePropertyChanged("Aircraft");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int EndSecond {
            get {
                return this.EndSecondField;
            }
            set {
                if ((this.EndSecondField.Equals(value) != true)) {
                    this.EndSecondField = value;
                    this.RaisePropertyChanged("EndSecond");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FlightID {
            get {
                return this.FlightIDField;
            }
            set {
                if ((object.ReferenceEquals(this.FlightIDField, value) != true)) {
                    this.FlightIDField = value;
                    this.RaisePropertyChanged("FlightID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FlightName {
            get {
                return this.FlightNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FlightNameField, value) != true)) {
                    this.FlightNameField = value;
                    this.RaisePropertyChanged("FlightName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int StartSecond {
            get {
                return this.StartSecondField;
            }
            set {
                if ((this.StartSecondField.Equals(value) != true)) {
                    this.StartSecondField = value;
                    this.RaisePropertyChanged("StartSecond");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AircraftInstance", Namespace="http://schemas.datacontract.org/2004/07/FlightDataEntities")]
    public partial class AircraftInstance : object, System.ComponentModel.INotifyPropertyChanged {
        
        private AircraftDataAnalysisWinRT.AircraftDataInput.AircraftModel AircraftModelField;
        
        private string AircraftNumberField;
        
        private System.DateTime LastUsedField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public AircraftDataAnalysisWinRT.AircraftDataInput.AircraftModel AircraftModel {
            get {
                return this.AircraftModelField;
            }
            set {
                if ((object.ReferenceEquals(this.AircraftModelField, value) != true)) {
                    this.AircraftModelField = value;
                    this.RaisePropertyChanged("AircraftModel");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AircraftNumber {
            get {
                return this.AircraftNumberField;
            }
            set {
                if ((object.ReferenceEquals(this.AircraftNumberField, value) != true)) {
                    this.AircraftNumberField = value;
                    this.RaisePropertyChanged("AircraftNumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime LastUsed {
            get {
                return this.LastUsedField;
            }
            set {
                if ((this.LastUsedField.Equals(value) != true)) {
                    this.LastUsedField = value;
                    this.RaisePropertyChanged("LastUsed");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AircraftModel", Namespace="http://schemas.datacontract.org/2004/07/FlightDataEntities")]
    public partial class AircraftModel : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string CaptionField;
        
        private System.DateTime LastUsedField;
        
        private string ModelNameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Caption {
            get {
                return this.CaptionField;
            }
            set {
                if ((object.ReferenceEquals(this.CaptionField, value) != true)) {
                    this.CaptionField = value;
                    this.RaisePropertyChanged("Caption");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime LastUsed {
            get {
                return this.LastUsedField;
            }
            set {
                if ((this.LastUsedField.Equals(value) != true)) {
                    this.LastUsedField = value;
                    this.RaisePropertyChanged("LastUsed");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ModelName {
            get {
                return this.ModelNameField;
            }
            set {
                if ((object.ReferenceEquals(this.ModelNameField, value) != true)) {
                    this.ModelNameField = value;
                    this.RaisePropertyChanged("ModelName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DecisionRecord", Namespace="http://schemas.datacontract.org/2004/07/FlightDataEntities.Decisions")]
    public partial class DecisionRecord : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string DecisionDescriptionField;
        
        private string DecisionIDField;
        
        private string DecisionNameField;
        
        private int EndSecondField;
        
        private string FlightIDField;
        
        private int StartSecondField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DecisionDescription {
            get {
                return this.DecisionDescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DecisionDescriptionField, value) != true)) {
                    this.DecisionDescriptionField = value;
                    this.RaisePropertyChanged("DecisionDescription");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DecisionID {
            get {
                return this.DecisionIDField;
            }
            set {
                if ((object.ReferenceEquals(this.DecisionIDField, value) != true)) {
                    this.DecisionIDField = value;
                    this.RaisePropertyChanged("DecisionID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DecisionName {
            get {
                return this.DecisionNameField;
            }
            set {
                if ((object.ReferenceEquals(this.DecisionNameField, value) != true)) {
                    this.DecisionNameField = value;
                    this.RaisePropertyChanged("DecisionName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int EndSecond {
            get {
                return this.EndSecondField;
            }
            set {
                if ((this.EndSecondField.Equals(value) != true)) {
                    this.EndSecondField = value;
                    this.RaisePropertyChanged("EndSecond");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FlightID {
            get {
                return this.FlightIDField;
            }
            set {
                if ((object.ReferenceEquals(this.FlightIDField, value) != true)) {
                    this.FlightIDField = value;
                    this.RaisePropertyChanged("FlightID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int StartSecond {
            get {
                return this.StartSecondField;
            }
            set {
                if ((this.StartSecondField.Equals(value) != true)) {
                    this.StartSecondField = value;
                    this.RaisePropertyChanged("StartSecond");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Level1FlightRecord", Namespace="http://schemas.datacontract.org/2004/07/FlightDataEntities")]
    public partial class Level1FlightRecord : object, System.ComponentModel.INotifyPropertyChanged {
        
        private int EndSecondField;
        
        private string FlightIDField;
        
        private string ParameterIDField;
        
        private int StartSecondField;
        
        private System.Collections.ObjectModel.ObservableCollection<float> ValuesField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int EndSecond {
            get {
                return this.EndSecondField;
            }
            set {
                if ((this.EndSecondField.Equals(value) != true)) {
                    this.EndSecondField = value;
                    this.RaisePropertyChanged("EndSecond");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FlightID {
            get {
                return this.FlightIDField;
            }
            set {
                if ((object.ReferenceEquals(this.FlightIDField, value) != true)) {
                    this.FlightIDField = value;
                    this.RaisePropertyChanged("FlightID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ParameterID {
            get {
                return this.ParameterIDField;
            }
            set {
                if ((object.ReferenceEquals(this.ParameterIDField, value) != true)) {
                    this.ParameterIDField = value;
                    this.RaisePropertyChanged("ParameterID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int StartSecond {
            get {
                return this.StartSecondField;
            }
            set {
                if ((this.StartSecondField.Equals(value) != true)) {
                    this.StartSecondField = value;
                    this.RaisePropertyChanged("StartSecond");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.ObjectModel.ObservableCollection<float> Values {
            get {
                return this.ValuesField;
            }
            set {
                if ((object.ReferenceEquals(this.ValuesField, value) != true)) {
                    this.ValuesField = value;
                    this.RaisePropertyChanged("Values");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="LevelTopFlightRecord", Namespace="http://schemas.datacontract.org/2004/07/FlightDataEntities")]
    public partial class LevelTopFlightRecord : object, System.ComponentModel.INotifyPropertyChanged {
        
        private int EndSecondField;
        
        private AircraftDataAnalysisWinRT.AircraftDataInput.ExtremumPointInfo ExtremumPointInfoField;
        
        private string FlightIDField;
        
        private System.Collections.ObjectModel.ObservableCollection<AircraftDataAnalysisWinRT.AircraftDataInput.Level2FlightRecord> Level2FlightRecordField;
        
        private string ParameterIDField;
        
        private int StartSecondField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int EndSecond {
            get {
                return this.EndSecondField;
            }
            set {
                if ((this.EndSecondField.Equals(value) != true)) {
                    this.EndSecondField = value;
                    this.RaisePropertyChanged("EndSecond");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public AircraftDataAnalysisWinRT.AircraftDataInput.ExtremumPointInfo ExtremumPointInfo {
            get {
                return this.ExtremumPointInfoField;
            }
            set {
                if ((object.ReferenceEquals(this.ExtremumPointInfoField, value) != true)) {
                    this.ExtremumPointInfoField = value;
                    this.RaisePropertyChanged("ExtremumPointInfo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FlightID {
            get {
                return this.FlightIDField;
            }
            set {
                if ((object.ReferenceEquals(this.FlightIDField, value) != true)) {
                    this.FlightIDField = value;
                    this.RaisePropertyChanged("FlightID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.ObjectModel.ObservableCollection<AircraftDataAnalysisWinRT.AircraftDataInput.Level2FlightRecord> Level2FlightRecord {
            get {
                return this.Level2FlightRecordField;
            }
            set {
                if ((object.ReferenceEquals(this.Level2FlightRecordField, value) != true)) {
                    this.Level2FlightRecordField = value;
                    this.RaisePropertyChanged("Level2FlightRecord");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ParameterID {
            get {
                return this.ParameterIDField;
            }
            set {
                if ((object.ReferenceEquals(this.ParameterIDField, value) != true)) {
                    this.ParameterIDField = value;
                    this.RaisePropertyChanged("ParameterID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int StartSecond {
            get {
                return this.StartSecondField;
            }
            set {
                if ((this.StartSecondField.Equals(value) != true)) {
                    this.StartSecondField = value;
                    this.RaisePropertyChanged("StartSecond");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ExtremumPointInfo", Namespace="http://schemas.datacontract.org/2004/07/FlightDataEntities")]
    public partial class ExtremumPointInfo : object, System.ComponentModel.INotifyPropertyChanged {
        
        private float MaxValueField;
        
        private float MaxValueSecondField;
        
        private float MinValueField;
        
        private float MinValueSecondField;
        
        private string ParameterIDField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public float MaxValue {
            get {
                return this.MaxValueField;
            }
            set {
                if ((this.MaxValueField.Equals(value) != true)) {
                    this.MaxValueField = value;
                    this.RaisePropertyChanged("MaxValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public float MaxValueSecond {
            get {
                return this.MaxValueSecondField;
            }
            set {
                if ((this.MaxValueSecondField.Equals(value) != true)) {
                    this.MaxValueSecondField = value;
                    this.RaisePropertyChanged("MaxValueSecond");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public float MinValue {
            get {
                return this.MinValueField;
            }
            set {
                if ((this.MinValueField.Equals(value) != true)) {
                    this.MinValueField = value;
                    this.RaisePropertyChanged("MinValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public float MinValueSecond {
            get {
                return this.MinValueSecondField;
            }
            set {
                if ((this.MinValueSecondField.Equals(value) != true)) {
                    this.MinValueSecondField = value;
                    this.RaisePropertyChanged("MinValueSecond");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ParameterID {
            get {
                return this.ParameterIDField;
            }
            set {
                if ((object.ReferenceEquals(this.ParameterIDField, value) != true)) {
                    this.ParameterIDField = value;
                    this.RaisePropertyChanged("ParameterID");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Level2FlightRecord", Namespace="http://schemas.datacontract.org/2004/07/FlightDataEntities")]
    public partial class Level2FlightRecord : object, System.ComponentModel.INotifyPropertyChanged {
        
        private float AvgValueField;
        
        private int CountField;
        
        private int EndSecondField;
        
        private AircraftDataAnalysisWinRT.AircraftDataInput.ExtremumPointInfo ExtremumPointInfoField;
        
        private string FlightIDField;
        
        private float MaxValueField;
        
        private float MinValueField;
        
        private string ParameterIDField;
        
        private int StartSecondField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public float AvgValue {
            get {
                return this.AvgValueField;
            }
            set {
                if ((this.AvgValueField.Equals(value) != true)) {
                    this.AvgValueField = value;
                    this.RaisePropertyChanged("AvgValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Count {
            get {
                return this.CountField;
            }
            set {
                if ((this.CountField.Equals(value) != true)) {
                    this.CountField = value;
                    this.RaisePropertyChanged("Count");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int EndSecond {
            get {
                return this.EndSecondField;
            }
            set {
                if ((this.EndSecondField.Equals(value) != true)) {
                    this.EndSecondField = value;
                    this.RaisePropertyChanged("EndSecond");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public AircraftDataAnalysisWinRT.AircraftDataInput.ExtremumPointInfo ExtremumPointInfo {
            get {
                return this.ExtremumPointInfoField;
            }
            set {
                if ((object.ReferenceEquals(this.ExtremumPointInfoField, value) != true)) {
                    this.ExtremumPointInfoField = value;
                    this.RaisePropertyChanged("ExtremumPointInfo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FlightID {
            get {
                return this.FlightIDField;
            }
            set {
                if ((object.ReferenceEquals(this.FlightIDField, value) != true)) {
                    this.FlightIDField = value;
                    this.RaisePropertyChanged("FlightID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public float MaxValue {
            get {
                return this.MaxValueField;
            }
            set {
                if ((this.MaxValueField.Equals(value) != true)) {
                    this.MaxValueField = value;
                    this.RaisePropertyChanged("MaxValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public float MinValue {
            get {
                return this.MinValueField;
            }
            set {
                if ((this.MinValueField.Equals(value) != true)) {
                    this.MinValueField = value;
                    this.RaisePropertyChanged("MinValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ParameterID {
            get {
                return this.ParameterIDField;
            }
            set {
                if ((object.ReferenceEquals(this.ParameterIDField, value) != true)) {
                    this.ParameterIDField = value;
                    this.RaisePropertyChanged("ParameterID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int StartSecond {
            get {
                return this.StartSecondField;
            }
            set {
                if ((this.StartSecondField.Equals(value) != true)) {
                    this.StartSecondField = value;
                    this.RaisePropertyChanged("StartSecond");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="AircraftDataInput.IAircraftDataInput")]
    public interface IAircraftDataInput {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAircraftDataInput/AddOrReplaceFlight", ReplyAction="http://tempuri.org/IAircraftDataInput/AddOrReplaceFlightResponse")]
        System.Threading.Tasks.Task<AircraftDataAnalysisWinRT.AircraftDataInput.Flight> AddOrReplaceFlightAsync(AircraftDataAnalysisWinRT.AircraftDataInput.Flight flight);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAircraftDataInput/DeleteExistsData", ReplyAction="http://tempuri.org/IAircraftDataInput/DeleteExistsDataResponse")]
        System.Threading.Tasks.Task<string> DeleteExistsDataAsync(AircraftDataAnalysisWinRT.AircraftDataInput.Flight flight);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAircraftDataInput/AddDecisionRecordsBatch", ReplyAction="http://tempuri.org/IAircraftDataInput/AddDecisionRecordsBatchResponse")]
        System.Threading.Tasks.Task<string> AddDecisionRecordsBatchAsync(AircraftDataAnalysisWinRT.AircraftDataInput.Flight flight, System.Collections.ObjectModel.ObservableCollection<AircraftDataAnalysisWinRT.AircraftDataInput.DecisionRecord> records);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAircraftDataInput/AddOneParameterValue", ReplyAction="http://tempuri.org/IAircraftDataInput/AddOneParameterValueResponse")]
        System.Threading.Tasks.Task<string> AddOneParameterValueAsync(AircraftDataAnalysisWinRT.AircraftDataInput.Flight flight, string parameterID, System.Collections.ObjectModel.ObservableCollection<AircraftDataAnalysisWinRT.AircraftDataInput.Level1FlightRecord> reducedRecords);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAircraftDataInput/AddLevelTopFlightRecords", ReplyAction="http://tempuri.org/IAircraftDataInput/AddLevelTopFlightRecordsResponse")]
        System.Threading.Tasks.Task<string> AddLevelTopFlightRecordsAsync(AircraftDataAnalysisWinRT.AircraftDataInput.Flight flight, System.Collections.ObjectModel.ObservableCollection<AircraftDataAnalysisWinRT.AircraftDataInput.LevelTopFlightRecord> topRecords);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAircraftDataInputChannel : AircraftDataAnalysisWinRT.AircraftDataInput.IAircraftDataInput, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AircraftDataInputClient : System.ServiceModel.ClientBase<AircraftDataAnalysisWinRT.AircraftDataInput.IAircraftDataInput>, AircraftDataAnalysisWinRT.AircraftDataInput.IAircraftDataInput {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public AircraftDataInputClient() : 
                base(AircraftDataInputClient.GetDefaultBinding(), AircraftDataInputClient.GetDefaultEndpointAddress()) {
            this.Endpoint.Name = EndpointConfiguration.BasicHttpBinding_IAircraftDataInput.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public AircraftDataInputClient(EndpointConfiguration endpointConfiguration) : 
                base(AircraftDataInputClient.GetBindingForEndpoint(endpointConfiguration), AircraftDataInputClient.GetEndpointAddress(endpointConfiguration)) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public AircraftDataInputClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(AircraftDataInputClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress)) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public AircraftDataInputClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(AircraftDataInputClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public AircraftDataInputClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Threading.Tasks.Task<AircraftDataAnalysisWinRT.AircraftDataInput.Flight> AddOrReplaceFlightAsync(AircraftDataAnalysisWinRT.AircraftDataInput.Flight flight) {
            return base.Channel.AddOrReplaceFlightAsync(flight);
        }
        
        public System.Threading.Tasks.Task<string> DeleteExistsDataAsync(AircraftDataAnalysisWinRT.AircraftDataInput.Flight flight) {
            return base.Channel.DeleteExistsDataAsync(flight);
        }
        
        public System.Threading.Tasks.Task<string> AddDecisionRecordsBatchAsync(AircraftDataAnalysisWinRT.AircraftDataInput.Flight flight, System.Collections.ObjectModel.ObservableCollection<AircraftDataAnalysisWinRT.AircraftDataInput.DecisionRecord> records) {
            return base.Channel.AddDecisionRecordsBatchAsync(flight, records);
        }
        
        public System.Threading.Tasks.Task<string> AddOneParameterValueAsync(AircraftDataAnalysisWinRT.AircraftDataInput.Flight flight, string parameterID, System.Collections.ObjectModel.ObservableCollection<AircraftDataAnalysisWinRT.AircraftDataInput.Level1FlightRecord> reducedRecords) {
            return base.Channel.AddOneParameterValueAsync(flight, parameterID, reducedRecords);
        }
        
        public System.Threading.Tasks.Task<string> AddLevelTopFlightRecordsAsync(AircraftDataAnalysisWinRT.AircraftDataInput.Flight flight, System.Collections.ObjectModel.ObservableCollection<AircraftDataAnalysisWinRT.AircraftDataInput.LevelTopFlightRecord> topRecords) {
            return base.Channel.AddLevelTopFlightRecordsAsync(flight, topRecords);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync() {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync() {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration) {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IAircraftDataInput)) {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration) {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IAircraftDataInput)) {
                return new System.ServiceModel.EndpointAddress("http://localhost:45240/DataInputService.svc");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding() {
            return AircraftDataInputClient.GetBindingForEndpoint(EndpointConfiguration.BasicHttpBinding_IAircraftDataInput);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress() {
            return AircraftDataInputClient.GetEndpointAddress(EndpointConfiguration.BasicHttpBinding_IAircraftDataInput);
        }
        
        public enum EndpointConfiguration {
            
            BasicHttpBinding_IAircraftDataInput,
        }
    }
}
