const express = require('express');
const cors = require('cors');
const app = express();
const port = 3000;

app.use(cors());

const colors = ["red", "blue", "green", "yellow", "black", "Magenta", "cyan"];

app.get('/', (req, res) => {
    const randomColor = colors[Math.floor(Math.random() * colors.length)];
    res.json({ cor: randomColor });
});

app.listen(port, () => {
    console.log(`Servidor React está rodando em http://localhost:${port}`);
});
