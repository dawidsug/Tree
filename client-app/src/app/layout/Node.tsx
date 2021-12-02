import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";
import { Button, Item, List, Menu} from "semantic-ui-react";
import agent from "../api/agent";
import {CreateLeaf} from "../common/CreateLeaf";
import {CreateNode} from "../common/CreateNode";
import {EditNode} from "../common/EditNode";

import { TreeNodeDto } from "../models/node";
import { useStore } from "../stores/store";
import Leaf from "./Leaf";

export type NodeProps = {
  parentId: string;
};

export default observer( function Node ({ parentId }: NodeProps)
{

  const {myStore} = useStore();
    const {deleteNode} = myStore;
  const apiCall = agent.Nodes.byId;

  const [childVisible, setChildVisible] = useState(false);
  const [node, setNode] = useState<TreeNodeDto>();
  const [edit, setEdit] = useState(false);
  const [create, setCreate] = useState(false);
  const [isCreating, setCreating] = useState(false);
  const [createLeaf, setCreateLeaf] = useState(false);
  const [del, setDelete] = useState(false);

  useEffect(() => {
    apiCall(parentId).then((response) => {
      setNode(response);
    });
  }, [apiCall, parentId]);

  const disableButton = (node?.childrenIds.length ?? 0) === 0;

  return (
    <>
      <List>
        <Menu>
          <Menu.Item as="a">
            <Button
              disabled={disableButton}
              onClick={() => setChildVisible(!childVisible)}
              size="mini"
            >
              <List.Icon name="angle right" />
            </Button>
          </Menu.Item>
          <Menu.Item as="a">
            <List.Icon name="folder" />
            <List.Header>{node?.name}</List.Header>
          </Menu.Item>
          <Menu.Item>
            <Button
              onClick={() => {setEdit(!edit); setEdit(!edit);}}
            >
              {!edit &&
              <List.Icon name="edit" />}
            </Button>
          </Menu.Item>
          <Menu.Item>
            <Button
              onClick={() => {setCreate(!create); setCreating(!create) ;}}
            >
              {!create &&
              <List.Icon name="file alternate" />}
            </Button>
          </Menu.Item>
          <Menu.Item>
            <Button
              onClick={() => {setCreateLeaf(!createLeaf); }}
            >
              {!createLeaf &&
              <List.Icon name="file alternate" />}
            </Button>
          </Menu.Item>
          <Menu.Item>
            <Button
              onClick={() => {setDelete(!del);}}
            >
              {!del &&
              <List.Icon name="delete" />}
            </Button>
          </Menu.Item>
        </Menu>
          {childVisible && node?.childrenIds.map((id, key) => <Node key={key} parentId={id} />)}
          {childVisible && node?.leafs.map((leaf, key) => <Leaf key={key} leaf={leaf}/>)}
        </List>
        <Item>
          {edit && !isCreating && <EditNode id={node!.id} node={node!}/>}
          {create && isCreating && <CreateNode id={node!.id}/>}
          {createLeaf && <CreateLeaf id={node!.id}/>}
          {del && deleteNode(node!.id)}
        </Item> 
    </>
  );
})
