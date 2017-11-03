﻿// <copyright file="RvtClassMapper.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.RvtAddin.Services
{
    using System;
    using System.Collections.Generic;

    using Autodesk.Revit.DB;

    using AutoMapper;

    using JetBrains.Annotations;

    using StarkBIM.SampleRevitApp.Model.Core;

    using Element = StarkBIM.SampleRevitApp.Model.Core.Element;
    using RvtElement = Autodesk.Revit.DB.Element;
    using RvtView = Autodesk.Revit.DB.View;
    using View = StarkBIM.SampleRevitApp.Model.Core.View;

    /// <inheritdoc />
    public class RvtClassMapper : IRvtClassMapper
    {
        [NotNull]
        private readonly Dictionary<Type, Type> _typeDictionary;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RvtClassMapper" /> class.
        /// </summary>
        public RvtClassMapper(Dictionary<Type, Type> configuration)
        {
            _typeDictionary = configuration;
        }

        public RvtClassMapper()
            : this(GetDefaultConfiguration())
        {
        }

        /// <inheritdoc />
        public Type GetMappedType<TRvt>()
        {
            var type = typeof(TRvt);

            return GetMappedType(type);
        }

        /// <inheritdoc />
        public Type GetMappedType(Type type)
        {
            var currentType = type;

            while (currentType != null)
            {
                if (_typeDictionary.TryGetValue(currentType, out Type matchedType))
                {
                    return matchedType;
                }

                // No match has been found, try a base type
                currentType = currentType.BaseType;
            }

            return null;
        }

        /// <inheritdoc />
        public Type GetMappedType<TRvt>(TRvt rvtObject)
        {
            // Don't trust the type given here. We want the actual type of the object
            var type = rvtObject.GetType();
            return GetMappedType(type);
        }

        /*
        /// <inheritdoc />
        public object Map(object nativeRvtObject)
        {
            throw new NotImplementedException();
        }*/

        /// <inheritdoc />
        public T Map<T>(object nativeRvtObject)
        {
            return Mapper.Map<T>(nativeRvtObject);
        }

        private static Dictionary<Type, Type> GetDefaultConfiguration()
        {
            return new Dictionary<Type, Type>
                {
                    { typeof(RvtElement), typeof(Element) },
                    { typeof(RvtView), typeof(View) },
                    { typeof(ViewSheet), typeof(Sheet) }
                };
        }
    }
}