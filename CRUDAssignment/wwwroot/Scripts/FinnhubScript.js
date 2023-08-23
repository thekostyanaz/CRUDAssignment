let parsedJson;
let token = document.querySelector("#FinnhubTokenId").value;
let symbol = document.querySelector("#StockSymbolId").value;
let socket = new WebSocket(`wss://ws.finnhub.io?token=${token}`);
socket.onopen = event => socket.send(JSON.stringify({ 'type': 'subscribe', 'symbol': `${symbol}` }));

socket.onmessage = event => {
    console.log('Message from server ', event.data);
    parsedJson = JSON.parse(event.data);
    document.querySelector("#StockPrice").innerHTML = getPrice(parsedJson);
};

socket.onclose = event => {
    if (event.wasClean) {
        alert(`Connection closed cleanly, code = ${event.code} reason=${event.reason}`);
        socket.send(JSON.stringify({ 'type': 'unsubscribe', 'symbol': `${symbol}` }));
    }
    else
    {
        alert('Connection died');
    }
}

const getPrice = parsedJson =>
{
    let priceToDisplay = 0;
    Object.values(parsedJson.data).forEach(element => {
        if (element.p > priceToDisplay) {
            priceToDisplay = element.p;
        }
    });

    return priceToDisplay.toFixed(2);
}
