using DemoWebApp.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DemoWebApp.Services
{
    public class InterestsService : IInterestsService
    {
        private readonly HttpClient _httpClient;
        private readonly IServiceHttpClient _serviceClient;
        private readonly ILogger<InterestsService> _logger;
        public InterestsService(HttpClient httpClient, IServiceHttpClient serviceClient, ILogger<InterestsService> logger)
        {
            _httpClient = httpClient;
            _serviceClient = serviceClient;
            _logger = logger;
        }

        public async Task<string> GetContentForInterest(string title)
        {
            try
            {
                var apiResult = await _serviceClient.Get<string>(_httpClient, $"api/v1/content/{title}");

                if (apiResult.Success)
                {
                    return apiResult.Result;
                }
                // log failure and return null
                _logger.LogDebug($"Could not get interest content for document {title}: {apiResult.ErrorMessage}");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error calling GetAllInterestContentsForUser({title}");
                throw;
            }
        }

        public async Task<IEnumerable<Interest>> GetAllInterestsForUser(string userName)
        {
            try
            {
                var apiResult = await _serviceClient.Get<List<Interest>>(_httpClient, $"api/v1/interests/{userName}");

                if (apiResult.Success)
                {
                    return apiResult.Result;
                }
                // log failure and return null
                _logger.LogDebug($"Could not get interests for document {userName}: {apiResult.ErrorMessage}");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error calling GetAllInterestsForUser({userName}");
                throw;
            }
        }

        public void Subscribe(string userName, Interest interest)
        {
            try
            {
                _serviceClient.Post(_httpClient, $"api/v1/subscribe/{userName}", interest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error calling Subscribe({userName}");
                throw;
            }
        }

        public void UnSubscribe(string userName, Interest interest)
        {
            try
            {
                _serviceClient.Post(_httpClient, $"api/v1/unsubscribe/{userName}", interest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error calling UnSubscribe({userName}");
                throw;
            }
        }
    }
}
