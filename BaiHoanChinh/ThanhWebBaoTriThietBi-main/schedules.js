// =========================================================
// AngularJS App: Quản lý Lịch Bảo Trì
// =========================================================
var app = angular.module("AppQuanLyBaoTri", []);

app.controller("LichBaoTriCtrl", function ($scope, $http) {

    const API_LICH = "https://localhost:44310/api/LichBaoTri";
    const API_TS   = "https://localhost:44310/api/TaiSan";
    const API_NV   = "https://localhost:44310/api/NhanVien";

    $scope.listLichBaoTri = [];
    $scope.listTaiSan = [];
    $scope.listNhanVien = [];
    $scope.lich = {};
    $scope.showModal = false;
    $scope.modalTitle = "";


    // =========================================================
    // 1️⃣ LOAD DỮ LIỆU
    // =========================================================
    $scope.LoadData = function () {

        let reqLich = $http({ method: "GET", url: API_LICH + "/get-all" });
        let reqTS   = $http({ method: "GET", url: API_TS + "/get-all" });
        let reqNV   = $http({ method: "GET", url: API_NV + "/get-all" });

        Promise.all([reqLich, reqTS, reqNV])
        .then(function ([lichRes, tsRes, nvRes]) {

            // Convert camelCase → PascalCase
            $scope.listLichBaoTri = lichRes.data.map(x => ({
                MaLich: x.maLich,
                MaTaiSan: x.maTaiSan,
                MaNV: x.maNV,
                TanSuat: x.tanSuat,
                SoNgayLapLai: x.soNgayLapLai,
                NgayKeTiep: x.ngayKeTiep ? x.ngayKeTiep.split("T")[0] : "",
                HieuLuc: x.hieuLuc
            }));

            $scope.listTaiSan = tsRes.data;
            $scope.listNhanVien = nvRes.data;

            // Gán tên tài sản / nhân viên
            $scope.listLichBaoTri.forEach(item => {
                let ts = $scope.listTaiSan.find(t => t.maTaiSan === item.MaTaiSan);
                let nv = $scope.listNhanVien.find(n => n.maNV === item.MaNV);

                item.tenTaiSan = ts ? ts.tenTaiSan : "Chưa gán";
                item.tenNhanVien = nv ? nv.hoTen : "Chưa gán";
            });

            $scope.$applyAsync();
        })
        .catch(err => {
            console.error("❌ Lỗi load dữ liệu:", err);
            alert("Không thể tải dữ liệu");
        });
    };


    // =========================================================
    // 2️⃣ MỞ MODAL THÊM
    // =========================================================
    $scope.OpenModal = function () {
        $scope.lich = {
            MaTaiSan: "",
            MaNV: "",
            TanSuat: "",
            SoNgayLapLai: 0,
            NgayKeTiep: "",
            HieuLuc: true
        };
        $scope.modalTitle = "Thêm lịch bảo trì";
        $scope.showModal = true;
    };


    // =========================================================
    // 3️⃣ MỞ MODAL SỬA
    // =========================================================
    $scope.EditLich = function (item) {
        $scope.lich = angular.copy(item);
        $scope.modalTitle = "Cập nhật lịch bảo trì";
        $scope.showModal = true;
    };


    // =========================================================
    // 4️⃣ ĐÓNG MODAL
    // =========================================================
    $scope.CloseModal = function () {
        $scope.showModal = false;
    };


    // =========================================================
    // 5️⃣ LƯU (CREATE / UPDATE)
    // =========================================================
    $scope.SaveLich = function () {

        if (!$scope.lich.MaTaiSan || !$scope.lich.TanSuat || !$scope.lich.NgayKeTiep) {
            alert("⚠️ Vui lòng nhập đầy đủ thông tin!");
            return;
        }

        let data = {
            MaTaiSan: $scope.lich.MaTaiSan,
            MaNV: $scope.lich.MaNV || null,
            TanSuat: $scope.lich.TanSuat,
            SoNgayLapLai: $scope.lich.SoNgayLapLai,
            NgayKeTiep: $scope.lich.NgayKeTiep,
            HieuLuc: ($scope.lich.HieuLuc == true)
        };

        let req;

        // UPDATE
        if ($scope.lich.MaLich) {
            req = $http({
                method: "PUT",
                url: API_LICH + "/update/" + $scope.lich.MaLich,
                data: data
            });
        }
        // CREATE
        else {
            req = $http({
                method: "POST",
                url: API_LICH + "/create",
                data: data
            });
        }

        req.then(res => {
            alert("✔ " + res.data.message);
            $scope.CloseModal();
            $scope.LoadData();
        })
        .catch(err => {
            console.error("❌ Lỗi lưu:", err);
            alert("Không thể lưu!");
        });
    };


    // =========================================================
    // 6️⃣ XÓA – FIX LỖI 400 HOÀN TOÀN
    // =========================================================
    $scope.DeleteLich = function (id) {
        if (!confirm("Bạn có chắc muốn xóa lịch này?")) return;

        $http({
            method: "DELETE",
            url: API_LICH + "/delete/" + id
        })
        .then(res => {
            alert("🗑️ " + res.data.message);
            $scope.LoadData();
        })
        .catch(err => {
            console.error("❌ Lỗi xóa:", err);
            alert("Không thể xóa!");
        });
    };

    // Chạy khi load trang
    $scope.LoadData();
});
