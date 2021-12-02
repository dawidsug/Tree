import { observer } from "mobx-react-lite";
import { useState } from "react";
import { Button, Item, List, Menu} from "semantic-ui-react";
import {EditLeaf} from "../common/EditLeaf";
import { Leaf } from "../models/leaf";
import { useStore } from "../stores/store";

export type NodeProps = {
  leaf: Leaf;
};

export default observer( function Leaf ({ leaf }: NodeProps)
{

  const {myStore} = useStore();
  const {deleteLeaf} = myStore;
  const [edit, setEdit] = useState(false);
  const [del, setDelete] = useState(false);

  return (
    <>
      <List>
        <Menu>
          <Menu.Item as="a">
            <List.Icon name="file" />
            <List.Header>{leaf.name}</List.Header>
          </Menu.Item>
          <Menu.Item as="a">
            <List.Header>{leaf.title}</List.Header>
          </Menu.Item>
          <Menu.Item as="a">
            <List.Header>{leaf.text}</List.Header>
          </Menu.Item>
          <Menu.Item>
            <Button
              onClick={() => {setEdit(!edit);}}
            >
              {!edit &&
              <List.Icon name="edit"  />
              }
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
        </List>
        <Item>
          {edit && <EditLeaf id={leaf!.id} oldLeaf={leaf}/>}
          {del && deleteLeaf(leaf!.id)}
        </Item> 
    </>
    
)});

