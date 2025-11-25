var app = angular.module("AppQuanLyBaoTri", []);

app.controller("KhachHangCtrl", function ($scope, $http) {

    const API = "https://localhost:44310/api/KhachHang";

    $scope.listKhachHang = [];
    $scope.page = 1;
    $scope.pageSize = 10;
    $scope.totalPages = 1;

    $scope.kh = {};
    $scope.showModal = false;
    $scope.modalTitle = "Thêm khách hàng";

    // ===================== LOAD PAGING =====================
    $scope.loadPaging = function () {
        $http.get(`${API}/paging?page=${$scope.page}&pageSize=${$scope.pageSize}`)
            .then(res => {
                $scope.listKhachHang = res.data.data;
                $scope.totalPages = res.data.totalPages;
            });
    };

    $scope.nextPage = function () {
        if ($scope.page < $scope.totalPages) {
            $scope.page++;
            $scope.loadPaging();
        }
    };

    $scope.prevPage = function () {
        if ($scope.page > 1) {
            $scope.page--;
            $scope.loadPaging();
        }
    };

    // ===================== EXPORT =====================
    $scope.exportExcel = function () {
        window.location = API + "/export";
    };

    // ===================== IMPORT =====================
    $scope.importExcel = function () {
        var file = document.getElementById("fileExcel").files[0];
        if (!file) return alert("Chọn file trước!");

        var formData = new FormData();
        formData.append("file", file);

        $http.post(API + "/import", formData, {
            headers: { "Content-Type": undefined }
        }).then(res => {
            alert(`Import thành công! Thêm: ${res.data.added} - Trùng: ${res.data.skipped}`);
            $scope.loadPaging();
        });
    };

    // ===================== CRUD =====================
    $scope.openModal = function () {
        $scope.kh = {};
        $scope.modalTitle = "Thêm khách hàng";
        $scope.showModal = true;
    };

    $scope.closeModal = function () {
        $scope.showModal = false;
    };

    $scope.editKhachHang = function (kh) {
        $scope.kh = angular.copy(kh);
        $scope.modalTitle = "Chỉnh sửa khách hàng";
        $scope.showModal = true;
    };

    $scope.saveKhachHang = function () {
        let data = angular.copy($scope.kh);

        if (data.maKH) {
            $http.put(`${API}/update/${data.maKH}`, data).then(() => {
                alert("Cập nhật thành công!");
                $scope.closeModal();
                $scope.loadPaging();
            });
        } else {
            $http.post(`${API}/create`, data).then(() => {
                alert("Thêm thành công!");
                $scope.closeModal();
                $scope.loadPaging();
            });
        }
    };

    $scope.deleteKhachHang = function (id) {
        if (!confirm("Bạn chắc chắn muốn xóa?")) return;

        $http.delete(`${API}/delete/${id}`).then(() => {
            alert("Xóa thành công!");
            $scope.loadPaging();
        });
    };

    // KHỞI TẠO
    $scope.loadPaging();
});
