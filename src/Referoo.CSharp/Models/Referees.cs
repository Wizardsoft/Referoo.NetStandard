﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Referoo.CSharp.Models
{
    public class Referees
    {
        #region Singleton Pattern
        private static object syncObject = new object();
        private static Referees instance = null;

        /// <summary>
        /// Singleton pattern implementation
        /// </summary>
        public static Referees Instance
        {
            get
            {
                lock (syncObject)
                {
                    if (instance == null)
                    {
                        instance = new Referees();
                    }
                }
                return instance;
            }
        }
        #endregion Singleton Pattern

        /// <summary>
        /// This endpoint will return a list of referee's linked to your account in order of creation date.
        /// </summary>
        /// <param name="offset">The number of items to skip before starting to collect the result set</param>
        /// <param name="limit">The numbers of items to return. Up to 50 items can be returned at a time.</param>
        /// <returns></returns>
        public GetRefereesResponse ListReferees(Int64? offset, Int64? limit)
        {
            var url = $"referees/?";

            if (offset != null)
                url += $"offset={offset}&";

            if (limit != null)
                url += $"limit={limit}&";

            var json = HttpHelpers.HttpGet(url);
            var retVal = JsonConvert.DeserializeObject<GetRefereesResponse>(json);
            return retVal;
        }

        /// <summary>
        /// This API endpoint will return a json object containing all of the referees a candidate has based on the record num provided in the API call.
        /// </summary>
        /// <param name="num">Numeric ID of the candidates referee's to retrieve</param>
        /// <returns></returns>
        public GetRefereesResponse RetrieveCandidatesReferees(Int64 num)
        {
            var url = $"candidate/{num}/referees";
            var json = HttpHelpers.HttpGet(url);
            var retVal = JsonConvert.DeserializeObject<GetRefereesResponse>(json);
            return retVal;
        }

        /// <summary>
        /// This API endpoint will return a json object containing a single candidate, based on the ID provided in the API call.
        /// </summary>
        /// <param name="num">Numeric ID of the referee to retrieve</param>
        /// <returns></returns>
        public GetRefereesResponse RetrieveSingleReferee(Int64 num)
        {
            var url = $"referee/{num}";
            var json = HttpHelpers.HttpGet(url);
            var retVal = JsonConvert.DeserializeObject<GetRefereesResponse>(json);
            return retVal;
        }

        /// <summary>
        /// This API endpoint will initiate the download of the referee's reference in PDF format.
        /// </summary>
        /// <param name="num">Numeric ID of the referees report to download.</param>
        /// <returns></returns>
        public string DownloadRefereesReference(Int64 num)
        {
            var url = $"referee/{num}/reference";
            var json = HttpHelpers.HttpGet(url);
            return json;
        }

        /// <summary>
        /// This endpoint will create a new referee under the candidate num that was passed in the URL.
        /// </summary>
        /// <param name="num">Numeric ID of the candidate to link the referee to</param>
        /// <param name="data">Data of referee to create</param>
        /// <returns></returns>
        public GetRefereesResponse CreateReferee(Int64 num, PostRefereeParameter data)
        {
            var url = $"candidate/{num}/referee";
            var json = HttpHelpers.HttpPost(url, data);
            var retVal = JsonConvert.DeserializeObject<GetRefereesResponse>(json);
            return retVal;
        }

        /// <summary>
        /// This endpoint will create a new candidate and create a new referee for them with one call. You can add up to 5 referee's per candidate. The referee will also be sent a reference request email.
        /// </summary>
        /// <param name="data">Data of candidate and referees to be created</param>
        /// <returns></returns>
        public GetCandidateWithRefereesResponse CreateCandidateWithReferees(PostCandidateWithRefereesParameter data)
        {
            var url = $"referee/quick";
            var json = HttpHelpers.HttpPost(url, data);
            var retVal = JsonConvert.DeserializeObject<GetCandidateWithRefereesResponse>(json);
            return retVal;
        }

        /// <summary>
        /// This API end point will update a candidate based on the ID passed in the call. Pass a JSON object containing the variables you wish to update.
        /// </summary>
        /// <param name="num">Numeric ID of the referee to update</param>
        /// <param name="data">Data of referee to update</param>
        /// <returns></returns>
        public GetRefereesResponse UpdateReferee(Int64 num, PutRefereeParameter data)
        {
            var url = $"referee/{num}";
            var json = HttpHelpers.HttpPut(url, data);
            var retVal = JsonConvert.DeserializeObject<GetRefereesResponse>(json);
            return retVal;
        }

        /// <summary>
        /// This API endpoint will send the candidate a reference request email.
        /// </summary>
        /// <param name="num">Numeric ID of the referee to email</param>
        /// <returns></returns>
        public string SendRefereeReferenceRequestEmail(Int64 num)
        {
            var url = $"referee/{num}/email";
            var json = HttpHelpers.HttpPut(url, null);
            return json;
        }
    }
}
