﻿namespace CardStorageService.Models.Requests
{
    public class CreateCardRequest
    {
        public int ClientId { get; set; }

        public string? CardN { get; set; }

        public string? Name { get; set; }

        public string? CVV2 { get; set; }

        public DateTime ExpDate { get; set; }

    }
}
