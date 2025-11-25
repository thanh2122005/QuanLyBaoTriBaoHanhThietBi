// =========================================================
// AngularJS App: Quản lý Tài sản (Frontend kết nối Backend ASP.NET)
// =========================================================
var app = angular.module('AppQuanLyBaoTri', []);

app.controller('TaiSanCtrl', function ($scope, $http) {

    const API = "https://localhost:44310/api/TaiSan";
    const API_KH = "https://localhost:44310/api/KhachHang";

    // ===== DỮ LIỆU =====
    $scope.listTaiSan = [];
    $scope.listKhachHang = [];
    $scope.asset = {};
    $scope.submitText = "Thêm mới";
    $scope.showModal = false;

    // ===== PHÂN TRANG =====
    $scope.page = 1;
    $scope.pageSize = 10;
    $scope.totalPages = 1;

    // =========================================================
    // LOAD KHÁCH HÀNG
    // =========================================================
    $scope.LoadKhachHang = function () {
        $http.get(API_KH + "/get-all").then(
            (res) => ($scope.listKhachHang = res.data),
            (err) => console.error("❌ Lỗi load khách hàng:", err)
        );
    };

    // =========================================================
    // LOAD PHÂN TRANG
    // =========================================================
    $scope.LoadTaiSan = function () {
        $http
            .get(`${API}/paging?page=${$scope.page}&pageSize=${$scope.pageSize}`)
            .then(
                (res) => {
                    $scope.listTaiSan = res.data.data;
                    $scope.totalPages = res.data.totalPages;
                },
                (err) => {
                    console.error("❌ Lỗi load tài sản:", err);
                    $scope.listTaiSan = [];
                }
            );
    };

    // ====== PREV PAGE ======
    $scope.prevPage = function () {
        if ($scope.page > 1) {
            $scope.page--;
            $scope.LoadTaiSan();
        }
    };

    // ====== NEXT PAGE ======
    $scope.nextPage = function () {
        if ($scope.page < $scope.totalPages) {
            $scope.page++;
            $scope.LoadTaiSan();
        }
    };

    // =========================================================
    // MỞ / ĐÓNG MODAL
    // =========================================================
    $scope.openModal = function () {
        $scope.asset = {};
        $scope.submitText = "Thêm mới";
        $scope.showModal = true;
    };

    $scope.closeModal = function () {
        $scope.showModal = false;
    };

    // =========================================================
    // SỬA
    // =========================================================
    $scope.EditTaiSan = function (ts) {
        $scope.asset = angular.copy(ts);
        $scope.submitText = "Cập nhật";
        $scope.showModal = true;

        if ($scope.asset.ngayMua) {
            $scope.asset.ngayMua = $scope.asset.ngayMua.split("T")[0];
        }
    };

    // =========================================================
    // LƯU (THÊM / CẬP NHẬT)
    // =========================================================
    $scope.SaveTaiSan = function () {
        const data = angular.copy($scope.asset);

        if (!data.tenTaiSan || !data.trangThai) {
            alert("⚠️ Vui lòng nhập đầy đủ thông tin!");
            return;
        }

        // ====== THÊM ======
        if ($scope.submitText === "Thêm mới") {
            $http
                .post(API + "/create", data)
                .then(
                    (res) => {
                        alert("✅ " + res.data.message);
                        $scope.closeModal();
                        $scope.LoadTaiSan();
                    },
                    (err) => {
                        console.error("❌ Lỗi thêm tài sản:", err);
                        alert("❌ Không thể thêm tài sản!");
                    }
                );
        }

        // ====== CẬP NHẬT ======
        else {
            $http
                .put(API + "/update/" + data.maTaiSan, data)
                .then(
                    (res) => {
                        alert("📝 " + res.data.message);
                        $scope.closeModal();
                        $scope.LoadTaiSan();
                    },
                    (err) => {
                        console.error("❌ Lỗi cập nhật tài sản:", err);
                        alert("❌ Không thể cập nhật tài sản!");
                    }
                );
        }
    };

    // =========================================================
    // XÓA
    // =========================================================
    $scope.DeleteTaiSan = function (id) {
        if (!confirm("🗑️ Bạn có chắc chắc muốn xóa tài sản này?")) return;

        $http.delete(API + "/delete/" + id).then(
            (res) => {
                alert("🗑️ " + res.data.message);
                $scope.LoadTaiSan();
            },
            (err) => {
                console.error("❌ Lỗi xóa:", err);
                alert("❌ Không thể xóa tài sản!");
            }
        );
    };

    // =========================================================
    // EXPORT EXCEL
    // =========================================================
    $scope.exportExcel = function () {
        window.open(API + "/export");
    };

  

    // =========================================================
    // KHỞI TẠO
    // =========================================================
    $scope.LoadKhachHang();
    $scope.LoadTaiSan();
});
