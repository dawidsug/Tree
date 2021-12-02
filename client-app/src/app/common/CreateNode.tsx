import { observer } from 'mobx-react-lite';
import React, { ChangeEvent, useState } from 'react';
import { Button, Form, Item } from 'semantic-ui-react';
import {v4 as uuid} from 'uuid';
import { useStore } from '../stores/store';
import agent from '../api/agent';

interface Props{
    id: string ;
}

export const CreateNode = ({id}: Props) =>
{

    const [node, setNode] = useState({
        id: '',
        name: '',
        parentId: id!
    });

    function handleSubmit(){
        let newNode = {
            ...node,
            id: uuid(),
            name: node.name,
            parentId: id!,
            isOpen: false,
            isNode: false
        };
        agent.Nodes.create(newNode)
        }
    
        function handleInputChange(event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>){
            const {name, value} = event.target;
            setNode({...node, [name]: value});
            
        }

    return(
        <>
            <Form onSubmit={handleSubmit} autoComplete='off'>
            <Item style={{display: 'flex', justifyContent:'center', marginBottom: '5px'}}>Create node</Item>
                <Form.Input placeholder='Name' value={node.name} name='name' onChange={handleInputChange}/>
                <Button onClick={() => {}} floated='right' positive type='submit' content='Submit'/>
                {/* <Button onClick={() => {}} floated='right' type='button' content='Cancel'/> */}
            </Form>
        </>
    )
}