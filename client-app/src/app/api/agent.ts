import axios, { AxiosError, AxiosResponse } from "axios";
import { toast } from "react-toastify";
import { history } from "../..";
import { Leaf } from "../models/leaf";
import { TreeNode, TreeNodeDto } from "../models/node";

axios.defaults.baseURL = "http://localhost:5000/api";

axios.interceptors.response.use(
  async (response) => {
    return response;
  },
  (error: AxiosError) => {
    const { data, status, config } = error.response!;
    switch (status) {
      case 400:
        if (typeof data === "string") {
          toast.error(data, {
            theme: "colored",
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
          });
        }
        if (config.method === "get" && data.errors.hasOwnProperty("id")) {
          history.push("/not-found");
        }
        if (data.errors) {
          const modalStateErrors = [];
          for (const key in data.errors) {
            if (data.errors[key]) {
              modalStateErrors.push(data.errors[key]);
            }
          }
          throw modalStateErrors.flat();
        }
        break;
      case 401:
        toast.error("unauthorised", {
          theme: "colored",
          hideProgressBar: false,
          closeOnClick: true,
          pauseOnHover: true,
          draggable: true,
          progress: undefined,
        });
        break;
      case 404:
        history.push("/not-found");
        toast.error("not found", {
          theme: "colored",
          hideProgressBar: false,
          closeOnClick: true,
          pauseOnHover: true,
          draggable: true,
          progress: undefined,
        });
        break;
      case 500:
        history.push("/server-error");
        break;
    }
    return Promise.reject(error);
  }
);

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

const requests = {
  get: <T>(url: string) => axios.get<T>(url).then(responseBody),
  post: <T>(url: string, body: {}) =>
    axios.post<T>(url, body).then(responseBody),
  put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
  putId: <T>(url: string) => axios.put<T>(url).then(responseBody),
  del: <T>(url: string) => axios.delete<T>(url).then(responseBody),
};

const Leafs = {
  list: (id: string) =>
    axios.get<Leaf[]>(`/leafs/${id}/leafs`).then(responseBody),
  details: (id: string) => requests.get<Leaf>(`/leafs/${id}`),
  create: (leaf: Leaf) => requests.post<void>("/leafs", leaf),
  update: (leaf: Leaf) => requests.put<void>(`/leafs/${leaf.id}`, leaf),
  delete: (id: string) => requests.del<void>(`/leafs/${id}`)
};

const Nodes = {
  byId: (id: string) =>
    axios.get<TreeNodeDto>(`/nodes/nodes/${id}`).then(responseBody),
  details: (id: string) => requests.get<TreeNodeDto>(`/nodes/${id}`),
  create: (node: TreeNode) => requests.post<void>("/nodes", node),
  update: (node: TreeNode) => requests.put<void>(`/nodes/${node.id}`, node),
  delete: (id: string) => requests.del<void>(`/nodes/${id}`),
  first: () => requests.get<string[]>(`/nodes/withoutParents`)
};

const agent = {
  Leafs,
  Nodes,
};

export default agent;
