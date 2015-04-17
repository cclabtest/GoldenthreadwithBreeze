(function () {
    angular.module('app').controller('VoicesController',
    ['$q', '$scope', '$timeout', 'VoiceDataService', 'logger', controller]);

    function controller($q, $scope, $timeout, VoiceDataService, logger) {
        var VV = this;
        VV.GetVoice = GetVoice;
        VV.GetAuthor = GetAuthor;
        VV.GetVoicesByStatus = GetVoicesByStatus;
        VV.GetTags = GetTags;

        VV.Voice = [];
        VV.Author = [];        
        VV.Tag = [];
        VV.Voice1 = [];

        GetVoice();
        GetAuthor();
        GetVoicesByStatus();
        GetTags();


        function GetVoice() {

            VoiceDataService.GetVoice()
                    .then(querySucceeded);
        }

        function GetAuthor() {

            VoiceDataService.GetAuthor()
                    .then(querySucceeded1);
        }

        function GetVoicesByStatus() {

            VoiceDataService.GetVoicesByStatus()
                    .then(querySucceeded3);
        }


        function GetTags() {

            VoiceDataService.GetTags()
                    .then(querySucceeded2);
        }

        function querySucceeded(data) {


            VV.Voice = data.results;

        }
        function querySucceeded1(data) {

            VV.Author = data.results;

        }
        function querySucceeded2(data) {

            VV.Tag = data.results;

        }

        function querySucceeded3(data) {

            VV.Voice1 = data.results;

        }
    };


}
)();