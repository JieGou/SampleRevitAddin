﻿// <copyright file="RvtTestSetBase.cs" company="StarkBIM Inc">
// Copyright (c) StarkBIM Inc. All rights reserved.
// </copyright>

namespace StarkBIM.SampleRevitApp.Commands.Test.SampleCommand
{
    using System;

    using Autodesk.Revit.UI;

    using JetBrains.Annotations;

    public abstract class RvtTestSetBase
    {
        [NotNull]
        private readonly ExternalCommandData _commandData;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RvtTestSetBase" /> class.
        /// </summary>
        protected RvtTestSetBase()
        {
            // Need to get the CommandData from the Test Runner
        }

        /// <summary>
        ///     Checks that the current context is valid for the given tests
        /// </summary>
        /// <returns>True if the tests should execute, otherwise false</returns>
        public abstract bool IsTestContextValid();

        [NotNull]
        protected ExternalCommandData GetCommandData()
        {
            // Do we want to throw an exception or return null here?
            if (!IsTestContextValid())
            {
                throw new InvalidOperationException("The context is not valid for this test");
            }

            return _commandData;
        }
    }
}