import React, { useState, useEffect } from "react";
import * as signalR from "@microsoft/signalr";

interface ICounter {
  hahaCounter?: number;
  heartCounter?: number;
  likeCounter?: number;
  tongueCounter?: number;
}

const App = () => {
  const [counters, setCounters] = useState<ICounter>({});
  const [hubConnection, setHubConnection] = useState<signalR.HubConnection>();
  const [hubStarted, setHubStarted] = useState(false);
  const hubUrl = process.env.REACT_APP_HUB_URL;

  useEffect(() => {
    if (hubUrl) {
      setHubConnection(
        new signalR.HubConnectionBuilder().withUrl(hubUrl).build()
      );
    }
  }, [hubUrl]);

  useEffect(() => {
    if (!!hubConnection && !hubStarted) {
      hubConnection
        .start()
        .then(() => {
          setHubStarted(true);
          console.log("connected");
          hubConnection.on("SendClickCounters", (counter: ICounter) => {
            setCounters(counter);
          });
        })
        .catch((err) => console.log({ err }));
    }
  }, [hubConnection, hubStarted]);

  const handleLikeClick = () =>
    hubConnection?.invoke("LikeClick", hubConnection?.connectionId);

  const handleHahaClick = () =>
    hubConnection?.invoke("HahaClick", hubConnection?.connectionId);

  const handleTongueClick = () =>
    hubConnection?.invoke("TongueClick", hubConnection?.connectionId);

  const handleHeartClick = () =>
    hubConnection?.invoke("HeartClick", hubConnection?.connectionId);

  return (
    <div className="group">
      <div className="hit">This is the SignalR in action</div>
      <div className="hit">Click Me!</div>
      <div className="box-container row">
        <div className="box-item col-xs-6" onClick={handleLikeClick}>
          <i className="fas fa-thumbs-up"></i>
          <span className="counter-like">{counters.likeCounter || 0}</span>
        </div>
        <div className="box-item col-xs-6" onClick={handleHahaClick}>
          <i className="fas fa-laugh"></i>
          <span className="counter-like">{counters.hahaCounter || 0}</span>
        </div>
        <div className="box-item col-xs-6" onClick={handleHeartClick}>
          <i className="fas fa-heart"></i>
          <span className="counter-like">{counters.heartCounter || 0}</span>
        </div>
        <div className="box-item col-xs-6" onClick={handleTongueClick}>
          <i className="fas fa-grin-tongue-wink"></i>
          <span className="counter-like">{counters.tongueCounter || 0}</span>
        </div>
      </div>
    </div>
  );
};

export default App;
