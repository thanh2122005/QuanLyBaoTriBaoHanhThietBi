var app = angular.module("AppQuanLyBaoTri", []);

app.controller("DashboardCtrl", function ($scope, $http) {

    const API = "https://localhost:44310/api/Dashboard/get-all";

    // ======================= KIỂM TRA ĐĂNG NHẬP =======================
    const user = JSON.parse(localStorage.getItem("userLogin"));
    if (!user) {
        window.location.href = "login.html";
        return;
    }

    document.getElementById("userInfo").innerText =
        `Xin chào: ${user.fullName || user.tenDangNhap} (${user.role})`;

    document.getElementById("logoutBtn").onclick = () => {
        localStorage.clear();
        window.location.href = "login.html";
    };

    // ======================= MODEL =======================
    $scope.stats = { taiSan: 0, dangLam: 0, suCo: 0, khachHang: 0 };
    $scope.alerts = [];
    $scope.lichSapToi = [];
    $scope.hoatDong = [];

    // ======================= LOAD DASHBOARD =======================
    $scope.loadDashboard = function () {

        $http.get(API).then(res => {

            const d = res.data;

            // ================= THỐNG KÊ =================
            $scope.stats.taiSan = d.thongKe.tongTaiSan;
            $scope.stats.dangLam = d.thongKe.dangLam;
            $scope.stats.suCo = d.thongKe.suCoChuaXL;
            $scope.stats.khachHang = d.thongKe.tongKhachHang;

            // ================= CẢNH BÁO =================
            $scope.alerts = [];

            if (d.canhBao.lichSapToi.length)
                $scope.alerts.push({
                    type: "warning",
                    message: `Có ${d.canhBao.lichSapToi.length} lịch bảo trì trong 7 ngày tới`
                });

            if (d.canhBao.bhSapHet.length)
                $scope.alerts.push({
                    type: "warning",
                    message: `${d.canhBao.bhSapHet.length} bảo hành sắp hết hạn`
                });

            // Lịch bảo trì sắp tới hiển thị ở cột phải
            $scope.lichSapToi = d.lichSapToi;

            // ================= HOẠT ĐỘNG =================
            $scope.hoatDong = [
                ...d.hoatDong.pcv.map(x => ({
                    loai: "Phiếu công việc",
                    tieuDe: x.tieuDe,
                    trangThai: x.trangThai,
                    ngay: x.ngayTao
                })),
                ...d.hoatDong.suco.map(x => ({
                    loai: "Sự cố",
                    tieuDe: x.moTa,
                    trangThai: x.trangThai,
                    ngay: x.ngayBaoCao
                }))
            ];

        }).catch(err => {
            console.error("Lỗi load Dashboard:", err);
            alert("Không thể tải Dashboard!");
        });
    };

    $scope.loadDashboard();
});
