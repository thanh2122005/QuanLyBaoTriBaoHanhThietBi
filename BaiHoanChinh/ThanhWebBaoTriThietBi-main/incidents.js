var app = angular.module("AppQuanLyBaoTri", []);

app.controller("SuCoCtrl", function ($scope, $http) {
  const baseUrl = "https://localhost:44310/api";
  const API_SUCO = baseUrl + "/PhieuSuCo";
  const API_TAISAN = baseUrl + "/TaiSan";
  const API_NHANVIEN = baseUrl + "/NhanVien";

  $scope.listSuCo = [];
  $scope.listTaiSan = [];
  $scope.listNhanVien = [];
  $scope.suco = {};
  $scope.showModal = false;
  $scope.modalTitle = "";

  // ===== LOAD =====
  $scope.LoadSuCo = function () {
    $http.get(API_SUCO + "/get-all").then(
      res => ($scope.listSuCo = res.data),
      err => console.error(err)
    );
  };

  $scope.LoadTaiSan = function () {
    $http.get(API_TAISAN + "/get-all").then(
      res => ($scope.listTaiSan = res.data),
      err => console.error(err)
    );
  };

  $scope.LoadNhanVien = function () {
    $http.get(API_NHANVIEN + "/get-all").then(
      res => ($scope.listNhanVien = res.data),
      err => console.error(err)
    );
  };

  // ===== MODAL =====
  $scope.openModal = function () {
    $scope.modalTitle = "Báo cáo Sự cố";
    $scope.suco = { mucDo: "Thấp", trangThai: "Mới" };
    $scope.showModal = true;
  };

  $scope.edit = function (sc) {
    $scope.modalTitle = "Cập nhật Sự cố";
    $scope.suco = angular.copy(sc);
    $scope.showModal = true;
  };

  $scope.closeModal = function () {
    $scope.showModal = false;
  };

  // ===== SAVE =====
  $scope.save = function () {
    if (!$scope.suco.maTaiSan || !$scope.suco.moTa) {
      alert("⚠️ Vui lòng nhập đầy đủ thông tin!");
      return;
    }

    let data = {
      maTaiSan: $scope.suco.maTaiSan,
      moTa: $scope.suco.moTa,
      mucDo: $scope.suco.mucDo,
      trangThai: $scope.suco.trangThai
    };

    let req;

    // UPDATE
    if ($scope.suco.maSuCo) {
      req = $http({
        method: "PUT",
        url: API_SUCO + "/update/" + $scope.suco.maSuCo,
        data: data
      });
    }
    // CREATE
    else {
      req = $http({
        method: "POST",
        url: API_SUCO + "/create",
        data: data
      });
    }

    req.then(
      res => {
        alert(res.data.message || "✔ Thành công!");
        $scope.closeModal();
        $scope.LoadSuCo();
      },
      err => {
        console.error(err);
        alert("❌ Không thể lưu dữ liệu!");
      }
    );
  };

  // ===== DELETE =====
  $scope.xoa = function (id) {
    if (!confirm("🗑️ Bạn có chắc muốn xóa sự cố này không?")) return;

    $http.delete(API_SUCO + "/" + id).then(
      res => {
        alert(res.data.message || "🗑️ Xóa thành công!");
        $scope.LoadSuCo();
      },
      err => {
        console.error(err);
        alert("❌ Xóa thất bại!");
      }
    );
  };

  // ===== GET NAME =====
  $scope.getTenTaiSan = function (id) {
    const ts = $scope.listTaiSan.find(x => x.maTaiSan === id);
    return ts ? ts.tenTaiSan : "Không xác định";
  };

  $scope.getTenNhanVien = function (id) {
    const nv = $scope.listNhanVien.find(x => x.maNV === id);
    return nv ? nv.hoTen : "Không xác định";
  };

  // ===== INIT =====
  $scope.LoadSuCo();
  $scope.LoadTaiSan();
  $scope.LoadNhanVien();
});
