code CONTRIBUTING.md
Quy tắc đóng góp
1. Quy trình đóng góp
Mỗi thành viên làm việc trên branch riêng thay vì trực tiếp trên main.

Khi hoàn thành, mở Pull Request (PR) để merge vào main.

PR cần được review bởi ít nhất một thành viên khác trước khi merge.

2. Quy tắc đặt tên branch
feature/... cho tính năng mới

fix/... cho sửa lỗi

docs/... cho tài liệu

Ví dụ: feature-login, fix-bug-level1, docs-readme

3. Quy tắc commit message
Commit message phải ngắn gọn, rõ ràng, mô tả đúng thay đổi.

Cấu trúc: <loại>: <mô tả>

Ví dụ:

feat: thêm màn hình đăng nhập

fix: sửa lỗi hiển thị nhân vật

docs: cập nhật README

4. Coding style
Tuân thủ chuẩn code của ngôn ngữ sử dụng (C#, JavaScript, Python, v.v.).

Code phải sạch, dễ đọc, có comment khi cần.

Tránh viết code lặp lại hoặc khó bảo trì.

5. Kiểm thử và CI/CD
Trước khi mở PR, chạy test để đảm bảo không lỗi.

GitHub Actions sẽ tự động kiểm tra khi có PR.

Nếu có lỗi, sửa trước khi merge.

6. Quản lý công việc
Dùng Issues để mô tả task.

Dùng Projects (Kanban board) để theo dõi tiến độ.

Gán task cho đúng người phụ trách.

Cập nhật trạng thái công việc thường xuyên.

7. Góp ý và thảo luận
Nếu có ý tưởng mới, mở Issue để thảo luận.

Tôn trọng ý kiến của nhau, cùng hướng tới chất lượng dự án.

Tránh push trực tiếp vào main nếu chưa review.

8. Tài liệu và hướng dẫn
Cập nhật README.md nếu có thay đổi lớn trong cấu trúc hoặc cách dùng.

Nếu thêm thư viện hoặc plugin, ghi rõ trong phần hướng dẫn cài đặt.