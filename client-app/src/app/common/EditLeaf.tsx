import { observer } from 'mobx-react-lite';
import React, { ChangeEvent, useEffect, useState } from 'react';
import { Button, Form, Item} from 'semantic-ui-react';
import agent from '../api/agent';
import { Leaf } from '../models/leaf';
import { useStore } from '../stores/store';

interface Props{
    id: string;
    oldLeaf: Leaf;
}

export const EditLeaf = ({id, oldLeaf}: Props) => 
{

    const [leaf, setLeaf] = useState({
        id: '',
        name: oldLeaf.name,
        title: oldLeaf.title,
        text: oldLeaf.text,
        parentId: oldLeaf.parentId
    });

    useEffect(() => {
        if (id) agent.Leafs.details(id).then(leaf => setLeaf(leaf!))
    }, [id]);

    function handleSubmit(){
            agent.Leafs.update(leaf)
        }
    
        function handleInputChange(event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>){
            const {name, value} = event.target;
            setLeaf({...leaf, [name]: value})
        }

    return(
        <>
            <Form onSubmit={handleSubmit} autoComplete='off'>
            <Item style={{display: 'flex', justifyContent:'center', marginBottom: '5px'}}>Edit leaf</Item>
                <Form.Input placeholder='Name' value={leaf.name} name='name' onChange={handleInputChange}/>
                <Form.Input placeholder='Title' value={leaf.title} name='title' onChange={handleInputChange}/>
                <Form.Input placeholder='Text' value={leaf.text} name='text' onChange={handleInputChange}/>
                <Button onClick={() => {}} floated='right' positive type='submit' content='Submit'/>
                {/* <Button onClick={() => {}} floated='right' type='button' content='Cancel'/> */}
            </Form>
        </>
    )
}