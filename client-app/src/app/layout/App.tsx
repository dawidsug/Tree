import React, { useEffect, useState } from "react";
import { Grid, Segment } from "semantic-ui-react";
import { observer } from "mobx-react-lite";
import { ToastContainer } from "react-toastify";
import Node from "./Node";
import agent from "../api/agent";

const App = () => {

  const apiCall = agent.Nodes.first;

  const [nodesWithoutParentsIds, setNodesWithoutParentsIds,] = useState<
    string[]
  >([]);

  useEffect(() => {
    apiCall().then((response) => {
      setNodesWithoutParentsIds(response);
    });
  }, [apiCall]);

  return (
    <>
      <ToastContainer position="bottom-right" hideProgressBar />
      <Grid verticalAlign="middle" centered={true}>
        <Segment>
        {nodesWithoutParentsIds.map((id, key) => (
              <Node key={key} parentId={id} />
            ))}
          </Segment>
      </Grid>
    </>
  );
};

export default observer(App);
