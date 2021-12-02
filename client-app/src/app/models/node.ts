import { Leaf } from "./leaf";

export interface TreeNode {
  id: string;
  name: string;
  parentId: string;
}

export interface TreeNodeDto {
  id: string;
  name: string;
  parentId: string;
  leafs: Leaf[];
  childrenIds: string[];
}
