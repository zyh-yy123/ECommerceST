import axios from 'axios';
import { ElMessage } from 'element-plus';

// 创建 Axios 实例
const http = axios.create({
  baseURL: 'http://localhost:5101', // 修改为后端实际运行的端口
  timeout: 5000,
  headers: {
    'Content-Type': 'application/json'
  }
});

// 请求拦截器：加入 Token（如果有）
http.interceptors.request.use(
  config => {
    const token = localStorage.getItem('token');
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  error => {
    return Promise.reject(error);
  }
);

// 响应拦截器：统一处理错误提示
http.interceptors.response.use(
  response => {
    // 直接返回 data 部分
    return response.data;
  },
  error => {
    if (error.response) {
      const msg = error.response.data?.message || '服务器返回错误';
      ElMessage.error(msg);
    } else {
      ElMessage.error('网络错误，请检查后重试');
    }
    return Promise.reject(error);
  }
);

export default http;

