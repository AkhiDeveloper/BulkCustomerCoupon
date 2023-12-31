﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace BulkCustomerCoupon.Console.APIClient
{
    internal class CouponAPIClient
    {
        private readonly RestClient _client;
        public CouponAPIClient()
        {
            var option = new RestClientOptions("http://coupon.dishhome.com.np/api");
            _client = new RestClient(option);
        }

        public async Task<bool> CreatePPVCoupon(string customerId, int couponTypeId)
        {
            try
            {
                var body = new { CustomerId = customerId, SchemeId="50", CouponTypeId=couponTypeId.ToString(), IsAlreadyRecharged=true, Remark = "Bulk PPV Coupon Generation" };
                var request = new RestRequest("PPVCoupon/Create").AddJsonBody(body);
                await _client.PostAsync(request);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

    }
}
