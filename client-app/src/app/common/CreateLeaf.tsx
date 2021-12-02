import { observer } from 'mobx-react-lite';
import React, { ChangeEvent, useState } from 'react';
import { Button, Form, Item, } from 'semantic-ui-react';
import {v4 as uuid} from 'uuid';
import agent from '../api/agent';
import { useStore } from '../stores/store';

interface Props{
    id: string;
}

export const CreateLeaf = ({id}: Props) => {

    const [leaf, setLeaf] = useState({
        id: '',
        name: '',
        title: '',
        text: '',
        parentId: ''
    });

    function handleSubmit(){
            let newLeaf = {
                ...leaf,
                id: uuid(),
                name: leaf.name,
                title: leaf.title,
                text: leaf.text,
                parentId: id
            };
            agent.Leafs.create(newLeaf)
        }
    
        function handleInputChange(event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>){
            const {name, value} = event.target;
            setLeaf({...leaf, [name]: value})
        }

    return(
        <>
            <Form onSubmit={handleSubmit} autoComplete='off'>
            <Item style={{display: 'flex', justifyContent:'center', marginBottom: '5px'}}>Create leaf</Item>
                <Form.Input placeholder='Name' value={leaf.name} name='name' onChange={handleInputChange}/>
                <Form.Input placeholder='Title' value={leaf.title} name='title' onChange={handleInputChange}/>
                <Form.Input placeholder='Text' value={leaf.text} name='text' onChange={handleInputChange}/>
                <Button onClick={() => {}} floated='right' positive type='submit' content='Submit'/>
                {/* <Button onClick={() => {}} floated='right' type='button' content='Cancel'/> */}
            </Form>
        </>
    )
}