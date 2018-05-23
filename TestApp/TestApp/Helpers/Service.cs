using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestApp.Models;

namespace TestApp.Helpers
{
    public class Service
    {
        #region Generic Post Method
        /// <summary>
        /// Generic post request to get response from server base on serSumQuantityvice model
        /// </summary>
        /// <typeparam name="T">Responce model class</typeparam>
        /// <param name="Url">Api url</param>
        /// <param name="Content">Post content data</param>
        /// <returns></returns>
        /// 

        //public async System.Threading.Tasks.Task<BaseResponse<T>> PostRequest<T>(string Url, StringContent Content, bool SentError = false)
        //{
        //    HttpClient client = new HttpClient();
        //    // client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        //    client.Timeout = TimeSpan.FromMinutes(5);
        //    if (!string.IsNullOrEmpty(Settings.Token))
        //    {
        //        client.DefaultRequestHeaders.Add("Authorization", Settings.Token);
        //    }
        //    BaseResponse<T> responce = new BaseResponse<T>();

        //    try
        //    {
        //        if (SentError)
        //            throw new System.Net.WebException();

        //        var result = await client.PostAsync(Url, Content);
        //        if ((int)result.StatusCode == 200)
        //        {
        //            var jsonString = result.Content.ReadAsStringAsync().Result;
        //            responce = JsonConvert.DeserializeObject<BaseResponse<T>>(jsonString);
        //            return responce;

        //        }
        //        else
        //        {
        //            // responce.Message = result.ReasonPhrase;
        //            responce.Message = result.ReasonPhrase;
        //            return responce;
        //        }
        //    }
        //    catch (System.Net.WebException ex)
        //    {
        //        responce.Message = ex.Message;

        //        return responce;
        //    }
        //    catch (System.Threading.Tasks.TaskCanceledException ex)
        //    {
        //        responce.Message = ex.Message;

        //        return responce;
        //    }
        //    catch (Exception exception)
        //    {
        //        responce.Message = exception.Message;
        //        return responce;
        //    }
        //    finally
        //    {
        //        client.Dispose();
        //    }
        //}


        #endregion

        #region Generic Post Method
        /// <summary>
        /// Generic post request to get response from server base on serSumQuantityvice model
        /// </summary>
        /// <typeparam name="T">Responce model class</typeparam>
        /// <param name="Url">Api url</param>
        /// <param name="Content">Post content data</param>
        /// <returns></returns>
        public async Task<JsonResponse<T>> PostRequest<T>(string Url, FormUrlEncodedContent Content, bool SentError = false)
        {
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(5);

            if (!string.IsNullOrEmpty(Settings.Token))
            {
                client.DefaultRequestHeaders.Add("Authorization", Settings.Token);
            }
            JsonResponse<T> responce = new JsonResponse<T>();

            try
            {
                if (SentError)
                    throw new System.Net.WebException();

                var result = await client.PostAsync(Url, Content);
                if ((int)result.StatusCode == 200)
                {

                    var jsonString = result.Content.ReadAsStringAsync().Result;
                    responce = JsonConvert.DeserializeObject<JsonResponse<T>>(jsonString);

                }
                return responce;
            }
            catch (System.Net.WebException ex)
            {
                // responce.ExceptionMessage = true;
                responce.Message = ex.Message;
                return responce;
            }
            catch (System.Threading.Tasks.TaskCanceledException ex)
            {
                //  responce.ExceptionMessage = true;
                responce.Message = ex.Message;
                return responce;
            }
            catch (Exception ex)
            {
                // responce.ExceptionMessage = true;
                responce.Message = ex.Message;
                return responce;
            }
            finally
            {
                client.Dispose();
            }
        }

        public async Task<JsonResponse<T>> CustomPostRequest<T>(string Url, FormUrlEncodedContent Content) where T : class, new()
        {

            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(5);

            if (!string.IsNullOrEmpty(Settings.Token))
            {
                client.DefaultRequestHeaders.Add("Authorization", Settings.Token);
            }
            JsonResponse<T> responce = new JsonResponse<T>();

            try
            {
                var result = await client.PostAsync(Url, Content);
                if ((int)result.StatusCode == 200)
                {
                    var jsonString = result.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(jsonString);
                    var data = JsonConvert.DeserializeObject<T>(jsonString);
                    responce.Data = data;

                }
                return responce;
                //else
                //{
                //   // responce.ExceptionMessage = true;
                //    responce.Message = "Server error, Please contact support team";
                //    return responce;
                //}

            }
            catch (System.Net.WebException ex)
            {
                //  responce.ExceptionMessage = true;
                responce.Message = ex.Message;

                return responce;
            }
            catch (System.Threading.Tasks.TaskCanceledException ex)
            {
                // responce.ExceptionMessage = true;
                responce.Message = ex.Message;
                return responce;
            }
            catch (Exception exception)
            {
                //responce.ExceptionMessage = true;
                responce.Message = exception.Message;
                return responce;
            }
            finally
            {
                client.Dispose();
                // UserDialogs.Instance.HideLoading();
            }
        }

        #endregion

        #region Generic Get Method
        public async Task<JsonResponse<T>> GetJsonRequest<T>(string Url) where T : class, new()
        {
            HttpClient client = new HttpClient();

            //var byteArray = Encoding.UTF8.GetBytes("YOApi:wG?0j59!");
            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            JsonResponse<T> responce = new JsonResponse<T>();

          
            if (!string.IsNullOrEmpty(Settings.Token))
            {
                //client.DefaultRequestHeaders.Add("Authorization", "Token 3204755d04a0f982ee26c4b827120ca5bb3420fc");
                //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
                //client.DefaultRequestHeaders.Add("key", "3204755d04a0f982ee26c4b827120ca5bb3420fc");
                string _ContentType = "application/json";
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_ContentType));
                client.DefaultRequestHeaders.Add("Authorization", "Token 3204755d04a0f982ee26c4b827120ca5bb3420fc");

            }

            client.Timeout = TimeSpan.FromMinutes(5);


            try
            {
                var result = await client.GetAsync(Url);
                if ((int)result.StatusCode == 200)
                {
                    var jsonString = result.Content.ReadAsStringAsync().Result;
                    responce = JsonConvert.DeserializeObject<JsonResponse<T>>(jsonString);
                    
                    return responce;
                }

                else
                {

                    responce.Message = "Server error, Please contact support team";
                    return responce;
                }
            }
            catch (System.Net.WebException ex)
            {
                // responce.ExceptionMessage = true;
                responce.Message = ex.Message;
                return responce;
            }
            catch (System.Threading.Tasks.TaskCanceledException ex)
            {
                // responce.ExceptionMessage = true;
                responce.Message = ex.Message;
                return responce;
            }
            catch (Exception exception)
            {
                // responce.ExceptionMessage = true;
                responce.Message = exception.Message;
                return responce;
            }
            finally
            {
                client.Dispose();
            }
        }
        #endregion

        public async Task<JsonResponse<TResult>> GenericPostMethod<TResult, TPost>(TPost post, string URL) where TPost : class
        {
            var postData = new List<System.Collections.Generic.KeyValuePair<string, string>>(); // Post Method

            // Get properties first
            var properties = post.GetType()
                                 .GetRuntimeProperties();

            foreach (var item in properties)
            {
                object value = item.GetValue(post); // Get values
                postData.Add(new KeyValuePair<string, string>(item.Name, Convert.ToString(value)));
            }
            // var data = GetContent(postData);
            // Post data and get result
            var result = await PostRequest<TResult>(URL, new FormUrlEncodedContent(postData));
            return result;

        }

        public FormUrlEncodedContent GetContent<T>(T Data) where T : class
        {
            var postData = new List<System.Collections.Generic.KeyValuePair<string, string>>(); // Post Method

            // Get properties first
            var properties = Data.GetType()
                                 .GetRuntimeProperties();

            foreach (var item in properties)
            {
                object value = item.GetValue(Data); // Get values
                postData.Add(new KeyValuePair<string, string>(item.Name, Convert.ToString(value)));
            }
            return new FormUrlEncodedContent(postData);
        }


    }
}