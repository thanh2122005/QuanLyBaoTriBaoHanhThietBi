var app = angular.module("AppQuanLyBaoTri", []);

app.controller("LogCtrl", function ($scope, $http) {
  const API_URL = "https://localhost:44310/api/NhatKyHeThong";

  // =======================
  // BIẾN PHÂN TRANG
  // =======================
  $scope.logs = [];
  $scope.totalItems = 0;
  $scope.totalPages = 0;

  $scope.page = 1;
  $scope.pageSize = 10;

  $scope.showModal = false;
  $scope.selectedLog = {};
  $scope.uniqueUsers = [];

  // =======================
  // LOAD LOG PHÂN TRANG
  // =======================
  $scope.loadLogs = function () {
    $http.get(`${API_URL}/paging?page=${$scope.page}&pageSize=${$scope.pageSize}`)
      .then(res => {

        $scope.logs = res.data.data;
        $scope.totalItems = res.data.totalItems;
        $scope.totalPages = res.data.totalPages;

        // danh sách người dùng duy nhất
        $scope.uniqueUsers = [...new Set($scope.logs.map(x => x.thayDoiBoi).filter(Boolean))];
      });
  };

  // =======================
  // ĐỔI TRANG
  // =======================
  $scope.changePage = function (newPage) {
    if (newPage < 1 || newPage > $scope.totalPages) return;
    $scope.page = newPage;
    $scope.loadLogs();
  };

  // =======================
  // FILTER LOG
  // =======================
  $scope.filterLogs = function (item) {
    const text = ($scope.searchText || "").toLowerCase();

    const matchSearch =
      item.tenBang?.toLowerCase().includes(text) ||
      item.hanhDong?.toLowerCase().includes(text) ||
      item.thayDoiBoi?.toLowerCase().includes(text);

    const matchAction = !$scope.filterAction || item.hanhDong === $scope.filterAction;
    const matchUser = !$scope.filterUser || item.thayDoiBoi === $scope.filterUser;

    return matchSearch && matchAction && matchUser;
  };

  // =======================
  // LẤY SỰ KHÁC BIỆT GIÁ TRỊ
  // =======================
  function extractChanges(oldValue, newValue) {
    let oldObj, newObj;

    try { oldObj = JSON.parse(oldValue || "{}"); }
    catch { return { old: oldValue, new: newValue }; }

    try { newObj = JSON.parse(newValue || "{}"); }
    catch { return { old: oldValue, new: newValue }; }

    const changedOld = {};
    const changedNew = {};

    for (let key in newObj) {
      if (oldObj[key] !== newObj[key]) {
        changedOld[key] = oldObj[key];
        changedNew[key] = newObj[key];
      }
    }

    return {
      old: Object.keys(changedOld).length ? JSON.stringify(changedOld, null, 2) : "—",
      new: Object.keys(changedNew).length ? JSON.stringify(changedNew, null, 2) : "—"
    };
  }

  // =======================
  // MODAL CHI TIẾT
  // =======================
  $scope.showDetail = function (log) {
    const result = extractChanges(log.giaTriCu, log.giaTriMoi);

    $scope.selectedLog = {
      ...log,
      giaTriCu: result.old,
      giaTriMoi: result.new
    };

    $scope.showModal = true;
  };

  $scope.closeModal = function () {
    $scope.showModal = false;
  };

  // =======================
  // KHỞI TẠO
  // =======================
  $scope.loadLogs();
});
