(function () {

    angular.module('app').controller('ChapterController',
    ['$q', '$scope', '$timeout', 'ChapterDataService', 'logger', controller]);

    function controller($q, $scope, $timeout, ChapterDataService, logger) {
        var VC = this;
        VC.GetChapter = GetChapter;
        VC.GetVoiceByChapter = GetVoiceByChapter;
        VC.items = [];
        VC.Voice = [];
        GetChapter();
        GetVoiceByChapter();

        function GetChapter() {

            ChapterDataService.GetChapter()
                    .then(querySucceeded);
        }

        function GetVoiceByChapter() {

            ChapterDataService.GetVoiceByChapter()
                    .then(querySucceeded1);
        }

        function querySucceeded(data) {

            VC.items = data.results;

        }
        function querySucceeded1(data) {

            VC.Voice = data.results;

        }
    };


}
)();