var app = angular.module("AppQuanLyBaoTri", []);

app.controller("InventoryCtrl", function ($scope, $http) {
  const API_URL = "https://localhost:44310/api/LinhKien";

  $scope.listLinhKien = [];
  $scope.allLinhKien = []; // giữ bản gốc để tìm kiếm
  $scope.linhKien = {};

  // ===== LOAD DỮ LIỆU =====
  $scope.loadData = function () {
    $http.get(API_URL + "/get-all").then(
      (res) => {
        const data = res.data.data || res.data || [];
        $scope.listLinhKien = data;
        $scope.allLinhKien = angular.copy(data);
      },
      (err) => console.error("❌ Lỗi tải dữ liệu:", err)
    );
  };

  // ===== LƯU (THÊM / SỬA) =====
  $scope.save = function () {
    if (!$scope.linhKien.tenLinhKien) {
      alert("⚠️ Vui lòng nhập tên linh kiện!");
      return;
    }

    const api = $scope.linhKien.maLinhKien
      ? $http.put(`${API_URL}/update/${$scope.linhKien.maLinhKien}`, $scope.linhKien)
      : $http.post(API_URL + "/create", $scope.linhKien);

    api.then(
      () => {
        alert("✅ Lưu thành công!");
        $scope.loadData();
        $scope.linhKien = {};
      },
      (err) => console.error("❌ Lỗi lưu:", err)
    );
  };

  // ===== CHỈNH SỬA =====
  $scope.edit = function (lk) {
    $scope.linhKien = angular.copy(lk);
  };

  // ===== XÓA =====
  $scope.delete = function (id) {
    if (confirm("Bạn có chắc muốn xóa linh kiện này không?")) {
      $http.delete(`${API_URL}/delete/${id}`).then(
        () => {
          alert("🗑️ Xóa thành công!");
          $scope.loadData();
        },
        (err) => {
          console.error("❌ Lỗi khi xóa:", err);
          alert("Không thể xóa linh kiện này!");
        }
      );
    }
  };

  // ===== TÌM KIẾM THEO TÊN HOẶC MÃ SỐ =====
  $scope.search = function () {
    const keyword = (
      ($scope.linhKien.tenLinhKien || "") + " " + ($scope.linhKien.maSo || "")
    )
      .toLowerCase()
      .trim();

    if (keyword === "") {
      $scope.listLinhKien = angular.copy($scope.allLinhKien);
      return;
    }

    $scope.listLinhKien = $scope.allLinhKien.filter(
      (x) =>
        (x.tenLinhKien && x.tenLinhKien.toLowerCase().includes(keyword)) ||
        (x.maSo && x.maSo.toLowerCase().includes(keyword))
    );
  };

  // ===== KHỞI TẠO =====
  $scope.loadData();
});
