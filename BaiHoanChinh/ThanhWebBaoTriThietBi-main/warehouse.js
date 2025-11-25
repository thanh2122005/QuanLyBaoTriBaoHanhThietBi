var app = angular.module("AppQuanLyBaoTri", []);

app.controller("WarehouseCtrl", function ($scope, $http) {

  const API = "https://localhost:44310/api/PhieuKho";
  const API_NV = "https://localhost:44310/api/NhanVien/get-all";

  $scope.listPhieuKho = [];
  $scope.listNhanVien = [];
  $scope.showModal = false;
  $scope.isEdit = false;

  // =========================================================
  // 1) LOAD DỮ LIỆU (FULL METHOD OBJECT)
  // =========================================================
  $scope.loadData = function () {

    // Load Phiếu kho
    $http({
      method: "GET",
      url: API + "/get-all"
    }).then(function (res) {

      res.data.forEach(x => {
        if (x.ngayLap)
          x.ngayLap = x.ngayLap.split("T")[0];
      });

      $scope.listPhieuKho = res.data;

    }, function (err) {
      console.error("❌ Lỗi load phiếu kho:", err);
    });

    // Load Nhân viên
    $http({
      method: "GET",
      url: API_NV
    }).then(function (res) {
      $scope.listNhanVien = res.data;
    }, function (err) {
      console.error("❌ Lỗi load nhân viên:", err);
    });
  };

  // =========================================================
  // 2) GET TÊN NHÂN VIÊN
  // =========================================================
  $scope.getTenNV = function (id) {
    const nv = $scope.listNhanVien.find(n => n.maNV == id);
    return nv ? nv.hoTen : "Không rõ";
  };

  // =========================================================
  // 3) CẬP NHẬT TÊN NHÂN VIÊN KHI CHỌN MÃ NV
  // =========================================================
  $scope.capNhatTenNhanVien = function () {
    const nv = $scope.listNhanVien.find(n => n.maNV == $scope.phieu.maNV);
    $scope.phieu.tenNhanVien = nv ? nv.hoTen : "";
  };

  // =========================================================
  // 4) OPEN MODAL THÊM
  // =========================================================
  $scope.openModal = function () {
    $scope.showModal = true;
    $scope.isEdit = false;
    $scope.modalTitle = "Thêm phiếu kho";

    $scope.phieu = {
      loai: "Nhap",
      ngayLap: new Date().toISOString().split("T")[0],
      maNV: "",
      tenNhanVien: "",
      ghiChu: ""
    };
  };

  // =========================================================
  // 5) OPEN MODAL SỬA
  // =========================================================
  $scope.editPhieuKho = function (p) {
    $scope.showModal = true;
    $scope.isEdit = true;
    $scope.modalTitle = "Cập nhật phiếu kho";

    $scope.phieu = angular.copy(p);
    $scope.phieu.ngayLap = p.ngayLap;
  };

  // =========================================================
  // 6) LƯU (THÊM / SỬA)
  // =========================================================
  $scope.save = function () {

    const data = {
      loai: $scope.phieu.loai,
      ngayLap: $scope.phieu.ngayLap,
      maNV: $scope.phieu.maNV,
      tenNhanVien: $scope.phieu.tenNhanVien,
      ghiChu: $scope.phieu.ghiChu
    };

    let req;

    if ($scope.isEdit) {
      // ---------- UPDATE ----------
      req = $http({
        method: "PUT",
        url: API + "/update/" + $scope.phieu.maPhieuKho,
        data: data
      });
    } else {
      // ---------- CREATE ----------
      req = $http({
        method: "POST",
        url: API + "/create",
        data: data
      });
    }

    req.then(
      function (res) {
        alert(res.data.message || "Thành công!");
        $scope.showModal = false;
        $scope.loadData();
      },
      function (err) {
        console.error("❌ Lỗi lưu:", err);
        alert("Không thể lưu phiếu kho!");
      }
    );
  };

  // =========================================================
  // 7) XÓA
  // =========================================================
  $scope.deletePhieuKho = function (id) {

    if (!confirm("Bạn có chắc chắn muốn xóa phiếu kho này không?"))
      return;

    $http({
      method: "DELETE",
      url: API + "/delete/" + id
    }).then(
      function (res) {
        alert(res.data.message);
        $scope.loadData();
      },
      function (err) {
        console.error("❌ Lỗi xóa:", err);
        alert("Không thể xóa phiếu kho!");
      }
    );
  };

  // =========================================================
  // 8) ĐÓNG MODAL  ⭐⭐ (BẠN ĐANG THIẾU PHẦN NÀY)
  // =========================================================
  $scope.closeModal = function () {
    $scope.showModal = false;
  };

  // =========================================================
  // 9) KHỞI TẠO
  // =========================================================
  $scope.loadData();

});
