var app = angular.module("AppQuanLyBaoTri", []);

app.controller("PCVChecklistCtrl", function ($scope, $http) {

    const API_PCV = "https://localhost:44310/api/PhieuCongViec/get-all";

    $scope.listPCV = [];

    $scope.LoadPCV = function () {

        $http.get(API_PCV).then(function (res) {

            let list = res.data.data || [];

            console.log("PCV:", list);

            // Tính tiến độ trực tiếp từ trạng thái
            list.forEach(p => {

                switch ((p.trangThai || "").trim()) {
                    case "Mới":
                        p.TienDo = 0;
                        break;
                    case "Đang xử lý":
                    case "Đang làm":
                        p.TienDo = 50;
                        break;
                    case "Hoàn thành":
                        p.TienDo = 100;
                        break;
                    default:
                        p.TienDo = 0;
                        break;
                }
            });

            $scope.listPCV = list;

        }).catch(function (err) {
            console.error("Lỗi load PCV:", err);
        });

    };

    $scope.LoadPCV();
});
