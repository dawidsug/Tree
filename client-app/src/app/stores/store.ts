import { createContext, useContext } from "react";
import MyStore from "./MyStore";

interface Store {
    myStore: MyStore;
}

export const store: Store ={
    myStore: new MyStore(),
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}