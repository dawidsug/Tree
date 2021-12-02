import { observer } from 'mobx-react-lite';
import React, { ChangeEvent, useEffect, useState } from 'react';
import { Button, Form, Item } from 'semantic-ui-react';
import { useStore } from '../stores/store';
import { TreeNodeDto } from '../models/node';
import agent from '../api/agent';

interface Props{
    id: string ;
    node: TreeNodeDto;
}

export const EditNode = ({id}: Props) =>
{

    const [node, setNode] = useState({
        id: id,
        name: '',
        parentId: id!
    });

    useEffect(() => {
        if (id) agent.Nodes.details(id).then(node => setNode(node))
    }, [id]);

    function handleSubmit(){
            console.log(node.name, node.id);
            agent.Nodes.update(node);
        }
    
        function handleInputChange(event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>){
            const {name, value} = event.target;
            setNode({...node, [name]: value});
            
        }

    return(
        <>
            <Form onSubmit={handleSubmit} autoComplete='off'>
            <Item style={{display: 'flex', justifyContent:'center', marginBottom: '5px'}}>Edit node</Item>
                <Form.Input placeholder='Name' value={node.name} name='name' onChange={handleInputChange}/>
                <Button onClick={() => {}} floated='right' positive type='submit' content='Submit'/>
                {/* <Button onClick={() => {}} floated='right' type='button' content='Cancel'/> */}
            </Form>
        </>
    )
}