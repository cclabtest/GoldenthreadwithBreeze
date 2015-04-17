(function () {

    angular.module('app').controller('ApplicationController',
    ['$q', '$scope', '$timeout', 'ApplicationDataService', 'logger', controller]);

    function controller($q, $scope, $timeout, ApplicationDataService, logger) {
        var VA = this;
        VA.GetApplication = GetApplication;
        VA.CreateApplication = CreateApplication;
        VA.items = [];
        
        var Application = {ID:0, APIKey:null, CreatedBy:1, CreatedDate:null};
        GetApplication();

        function CreateApplication()
        {
            ApplicationDataService.CreateApplication(Application);
            GetApplication();
        }
        function GetApplication() {           
            
            ApplicationDataService.GetApplication()
                    .then(querySucceeded);
            }

            function querySucceeded(data) {

                VA.items = data.results;
              
            }
        };


    }
)();