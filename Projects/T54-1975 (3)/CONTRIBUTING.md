code CONTRIBUTING.md
# Quy tắc đóng góp cho dự án

Cảm ơn bạn đã quan tâm và muốn đóng góp cho dự án **Vietnam's Road To Reunification**.  
Để làm việc nhóm hiệu quả, vui lòng tuân theo các quy tắc sau:

---

## 1. Quy trình làm việc
- Mỗi thành viên làm việc trên **branch riêng**:
  ```bash
  git checkout -b feature-tenchucnang
Khi hoàn thành, mở Pull Request để merge vào main.

Pull Request phải được review bởi ít nhất 1 thành viên khác.

2. Quy tắc đặt tên branch
feature/... cho tính năng mới

fix/... cho sửa lỗi

docs/... cho tài liệu

Ví dụ: feature-login, fix-bug-level1, docs-readme

3. Quy tắc commit message
Viết ngắn gọn, rõ ràng, bằng tiếng Anh hoặc tiếng Việt.

Cấu trúc: <loại>: <mô tả>

Ví dụ:

feat: thêm màn hình đăng nhập

fix: sửa lỗi hiển thị nhân vật

docs: cập nhật README

4. Coding style
Tuân thủ chuẩn code của ngôn ngữ bạn dùng (C#, JavaScript, Python, v.v.).

Giữ code sạch, dễ đọc, có comment khi cần.

Tránh viết code lặp lại hoặc khó bảo trì.

5. Kiểm thử và CI/CD
Trước khi mở Pull Request, chạy test để đảm bảo không lỗi.

GitHub Actions sẽ tự động kiểm tra khi có Pull Request.

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

Mã

---

### 3. Lưu và push lại lên GitHub