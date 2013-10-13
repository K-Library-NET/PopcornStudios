using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Telerik.Data
{
    /// <summary>
    /// Dynamic Object Builder
    /// </summary>
    internal class DynamicObjectBuilder
    {
        #region Private Members
        private static readonly Dictionary<TypeSignature, Type> TypesCache = new Dictionary<TypeSignature, Type>();

        private static readonly AssemblyBuilder MicroModelAssemblyBuilder =
            AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName("DynamicObjects"), AssemblyBuilderAccess.Run);

        private static readonly ModuleBuilder MicroModelModuleBuilder =
            MicroModelAssemblyBuilder.DefineDynamicModule("DynamicObjectsModule", true);

        private static readonly MethodInfo GetValueMethod =
            typeof(DynamicObject).GetMethod("GetValue", BindingFlags.Instance | BindingFlags.NonPublic);
        private static readonly MethodInfo SetValueMethod =
            typeof(DynamicObject).GetMethod("SetValue", BindingFlags.Instance | BindingFlags.NonPublic); 
        #endregion

        #region public static Type GetDynamicObjectBuilderType(IEnumerable<SLDataColumn> properties)
        /// <summary>
        /// Gets the type of the dynamic object builder.
        /// </summary>
        /// <param name="properties">The properties.</param>
        /// <returns></returns>
        public static Type GetDynamicObjectBuilderType(IEnumerable<SLDataColumn> properties)
        {
            Type type;
            var signature = new TypeSignature(properties);

            if (!TypesCache.TryGetValue(signature, out type))
            {
                type = CreateDynamicObjectBuilderType(properties);
                TypesCache.Add(signature, type);
            }

            return type;
        } 
        #endregion

        #region private static Type CreateDynamicObjectBuilderType(IEnumerable<SLDataColumn> columns)
        /// <summary>
        /// Creates the type of the dynamic object builder.
        /// </summary>
        /// <param name="columns">The columns.</param>
        /// <returns></returns>
        private static Type CreateDynamicObjectBuilderType(IEnumerable<SLDataColumn> columns)
        {
            var typeBuilder =
                MicroModelModuleBuilder.DefineType("DynamicObjectBuilder_" + Guid.NewGuid(), TypeAttributes.Public, typeof(DynamicObject));

            foreach (var property in columns)
            {
                var propertyBuilder = typeBuilder.DefineProperty(property.ColumnName, PropertyAttributes.None, property.DataType, null);

                CreateGetter(typeBuilder, propertyBuilder, property);
                CreateSetter(typeBuilder, propertyBuilder, property);
            }

            return typeBuilder.CreateType();
        } 
        #endregion

        #region private static void CreateGetter(TypeBuilder typeBuilder, PropertyBuilder propertyBuilder, SLDataColumn column)
        /// <summary>
        /// Creates the getter.
        /// </summary>
        /// <param name="typeBuilder">The type builder.</param>
        /// <param name="propertyBuilder">The property builder.</param>
        /// <param name="column">The column.</param>
        private static void CreateGetter(TypeBuilder typeBuilder, PropertyBuilder propertyBuilder, SLDataColumn column)
        {
            var getMethodBuilder = typeBuilder.DefineMethod(
                "get_" + column.ColumnName,
                MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName,
                CallingConventions.HasThis,
                column.DataType, Type.EmptyTypes);

            var getMethodIL = getMethodBuilder.GetILGenerator();
            getMethodIL.Emit(OpCodes.Ldarg_0);
            getMethodIL.Emit(OpCodes.Ldstr, column.ColumnName);
            getMethodIL.Emit(OpCodes.Callvirt, GetValueMethod.MakeGenericMethod(column.DataType));
            getMethodIL.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(getMethodBuilder);
        } 
        #endregion

        #region private static void CreateSetter(TypeBuilder typeBuilder, PropertyBuilder propertyBuilder, SLDataColumn column)
        /// <summary>
        /// Creates the setter.
        /// </summary>
        /// <param name="typeBuilder">The type builder.</param>
        /// <param name="propertyBuilder">The property builder.</param>
        /// <param name="column">The column.</param>
        private static void CreateSetter(TypeBuilder typeBuilder, PropertyBuilder propertyBuilder, SLDataColumn column)
        {
            var setMethodBuilder = typeBuilder.DefineMethod(
                "set_" + column.ColumnName,
                MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName,
                CallingConventions.HasThis,
                null, new[] { column.DataType });

            var setMethodIL = setMethodBuilder.GetILGenerator();
            setMethodIL.Emit(OpCodes.Ldarg_0);
            setMethodIL.Emit(OpCodes.Ldstr, column.ColumnName);
            setMethodIL.Emit(OpCodes.Ldarg_1);
            setMethodIL.Emit(OpCodes.Callvirt, SetValueMethod.MakeGenericMethod(column.DataType));
            setMethodIL.Emit(OpCodes.Ret);

            propertyBuilder.SetSetMethod(setMethodBuilder);
        } 
        #endregion
    }
}
