﻿// This file was auto-generated by ML.NET Model Builder. 
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
namespace ModelANG_WebApi1
{
    public partial class ModelANG
    {
        /// <summary>
        /// model input class for ModelANG.
        /// </summary>
        #region model input class
        public class ModelInput
        {
            [ColumnName(@"delta")]
            public float Delta { get; set; }

            [ColumnName(@"theta")]
            public float Theta { get; set; }

            [ColumnName(@"lowAlpha")]
            public float LowAlpha { get; set; }

            [ColumnName(@"highAlpha")]
            public float HighAlpha { get; set; }

            [ColumnName(@"lowBeta")]
            public float LowBeta { get; set; }

            [ColumnName(@"highBeta")]
            public float HighBeta { get; set; }

            [ColumnName(@"lowGamma")]
            public float LowGamma { get; set; }

            [ColumnName(@"highGamma")]
            public float HighGamma { get; set; }

            [ColumnName(@"label")]
            public string Label { get; set; }

        }

        #endregion

        /// <summary>
        /// model output class for ModelANG.
        /// </summary>
        #region model output class
        public class ModelOutput
        {
            [ColumnName("PredictedLabel")]
            public string Prediction { get; set; }

            public float[] Score { get; set; }
        }

        #endregion

        private static string MLNetModelPath = Path.GetFullPath("ModelANG.zip");

        public static readonly Lazy<PredictionEngine<ModelInput, ModelOutput>> PredictEngine = new Lazy<PredictionEngine<ModelInput, ModelOutput>>(() => CreatePredictEngine(), true);

        /// <summary>
        /// Use this method to predict on <see cref="ModelInput"/>.
        /// </summary>
        /// <param name="input">model input.</param>
        /// <returns><seealso cref=" ModelOutput"/></returns>
        public static ModelOutput Predict(ModelInput input)
        {
            var predEngine = PredictEngine.Value;
            return predEngine.Predict(input);
        }

        private static PredictionEngine<ModelInput, ModelOutput> CreatePredictEngine()
        {
            var mlContext = new MLContext();
            ITransformer mlModel = mlContext.Model.Load(MLNetModelPath, out var _);
            return mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
        }
    }
}