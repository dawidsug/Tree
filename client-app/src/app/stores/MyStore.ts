import agent from "../api/agent";

export default class MyStore{

    deleteNode = async (id: string) => {
        try {
            await agent.Nodes.delete(id);
        } catch (error) {
            console.log(error);
        }
    }

    deleteLeaf = async (id: string) => {
        try {
            await agent.Leafs.delete(id);
        } catch (error) {
            console.log(error);
        }
    }

}
    