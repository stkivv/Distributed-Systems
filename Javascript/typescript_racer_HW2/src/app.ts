import { RoadRow } from "./domain/road-row";
import { Game } from "./game/game";
import './app.css';

let uiGameBoard = document.querySelector("#app")!;
let uiScoreCounter = document.querySelector("#score")!;
let uiCar = document.getElementById("car")!;
let wrapper = document.getElementsByClassName("wrapper")[0];
let gameOverFlag = false;
let carPosX = 50;   
let carPosY = 85;

let game = new Game();

//=========================SETTING UP PAST SCORES================================
let url_string = window.location.href
let url = new URL(url_string);
let historyParam = url.searchParams.get("history");

if (historyParam) {
    let history = JSON.parse(historyParam)
    game.state.pastscores = history;
}
//=========================================================

let rowHeight = window.innerHeight / Game.MAP_ROWS;


//==============================CONTROLS INFO===========================
let controlsInfo = document.createElement('div');
controlsInfo.className = "controls"
controlsInfo.innerHTML = "CONTROLS:"

let controlsInfoContents1 = document.createElement('p');
controlsInfoContents1.className = "controls-contents";
controlsInfoContents1.innerHTML = "arrow keys left-right to move";
controlsInfo.appendChild(controlsInfoContents1);

let controlsInfoContents2 = document.createElement('p');
controlsInfoContents2.className = "controls-contents";
controlsInfoContents2.innerHTML = "space to start/pause";
controlsInfo.appendChild(controlsInfoContents2);

let controlsInfoContents3 = document.createElement('p');
controlsInfoContents3.className = "controls-contents";
controlsInfoContents3.innerHTML = "enter to reset";
controlsInfo.appendChild(controlsInfoContents3);

wrapper.appendChild(controlsInfo);
//===================================================================



//========================STARTSCREEN=========================
let startScreen = document.createElement('start')
startScreen.className = "start"
startScreen.innerHTML = "Racing Game"

let startScreenContents = document.createElement('p')
startScreenContents.className = "start-contents"
startScreenContents.innerHTML = "Press space to start"

startScreen.appendChild(startScreenContents)
wrapper.appendChild(startScreen)

displayScoreboard();
//============================================================



function uiAddGameRow(roadRow : RoadRow){
    let rowDiv = document.createElement('div');
    rowDiv.className = "row"

    let leftDiv = document.createElement('div');
    let rightDiv = document.createElement('div');
    let roadDiv = document.createElement('div');
    leftDiv.className = "left-edge";
    rightDiv.className = "right-edge";
    roadDiv.className = "road";

    rowDiv.style.height = rowHeight + 'px';

    leftDiv.style.width = (roadRow.leftEdge * 100)  + '%'
    roadDiv.style.width = ((roadRow.rightEdge - roadRow.leftEdge) * 100) + '%'
    rightDiv.style.width = ((1 - roadRow.rightEdge) * 100) + '%'
    

    if (roadRow.obstacle) {
        let obstacleDiv = document.createElement('div')
        obstacleDiv.className = "obstacle"
        obstacleDiv.style.height = rowHeight + 'px';
        obstacleDiv.style.width = 5 + 'vw'

        let roadLeft = document.createElement('div');
        let roadRight = document.createElement('div');
        roadLeft.className = "road";
        roadRight.className = "road";

        roadLeft.style.width = ((roadRow.obstacle - roadRow.leftEdge - 0.025) * 100) + '%'
        roadRight.style.width = ((roadRow.rightEdge - roadRow.obstacle - 0.025) * 100) + '%'

        rowDiv.appendChild(leftDiv);
        rowDiv.appendChild(roadLeft);
        rowDiv.appendChild(obstacleDiv);
        rowDiv.appendChild(roadRight);
        rowDiv.appendChild(rightDiv);
    } else {
        rowDiv.appendChild(leftDiv);
        rowDiv.appendChild(roadDiv);
        rowDiv.appendChild(rightDiv);
    }

    return rowDiv;
}


//=========================INITIAL ROWS================================
for (let index = Game.MAP_ROWS - 1; index >= 0; index--) {
    uiAddGameRow(game.state.map[index])
    uiGameBoard.appendChild(uiAddGameRow(game.state.map[index]));
}
//=========================================================



//=========================COLLISION DETECTION================================
function detectCollision(){
    for (let index = 1; index < 5; index++) {
        let row = game.state.map[index];
        if ((row.leftEdge - 0.025) * 100 >= carPosX || (row.rightEdge - 0.025) * 100 <= carPosX) {
            gameOver();

            console.log("COLLISION EDGE!");
            console.log(row.leftEdge  * 100 + "," + row.rightEdge * 100);
            console.log(carPosX);
        }
        if (row.obstacle) {
            if ((row.obstacle - 0.07) * 100 < carPosX && (row.obstacle) * 100 > carPosX){
                gameOver();

                console.log("COLLISION OBSTACLE!");
            }
        }
    }
}
//=========================================================



//========================INTERVALS(SCORE/SPEED, ROW GENERATION)=================================
let bgCounter = 0;
let obstacleCounterDefault = 3; //this is used to avoid obstacles spawning too close to each other and making the game unwinnable
let obstacleCounter = obstacleCounterDefault;

function uiIntervalContents() {
    if (!game.state.is_running) {
        return;
    }

    if (obstacleCounter == 0) {
        game.addNewMapRow(true);
        obstacleCounter = obstacleCounterDefault;
    } else {
        game.addNewMapRow(false);
        obstacleCounter--;
    }


    uiGameBoard.childNodes[uiGameBoard.childNodes.length - 1].remove();
    uiGameBoard.prepend(uiAddGameRow(game.state.map[Game.MAP_ROWS - 1]));

    document.body.style.backgroundPositionY = (bgCounter + rowHeight).toString() + "px"
    bgCounter = bgCounter + rowHeight;

    detectCollision();
}



let uiRowsInterval = setInterval(uiIntervalContents, game.state.speed);


let score = 0;
let uiScoreAndSpeedInterval = setInterval(() => {
    if (!game.state.is_running) {
        return;
    }
    score++;
    uiScoreCounter.innerHTML = 'SCORE: ' + score.toString();
    game.state.currentscore = score;

    if (game.state.speed > 35) {
        game.state.speed = game.state.speed - 15;
    }

    clearInterval(uiRowsInterval);
    uiRowsInterval = setInterval(uiIntervalContents, game.state.speed)
}, 2000);
//=========================================================



//===========================CONTROLS==============================
document.addEventListener('keydown', (event) => {
    let name = event.key;
    console.log(name + " pressed")

    if ((name == 'ArrowLeft' || name == 'a') && game.state.is_running) {
        carPosX--;
        uiCar.style.left = (carPosX).toString() + "vw";
    }

    if ((name == 'ArrowRight' || name == 'd') && game.state.is_running) {
        carPosX++;
        uiCar.style.left = (carPosX).toString() + "vw";
    }

    if (name == ' ' && !gameOverFlag) {
        if (game.state.is_running == false) {
            unpause();
        } else {
            pause();
        }
    }

    if(name == 'Enter'){
        restartGame();
    }
})
//=========================================================




function gameOver(){
    game.state.is_running = false;
    game.state.pastscores.push(game.state.currentscore);
    gameOverFlag = true;

    let ensureOver = document.getElementsByClassName("gameover");

    displayScoreboard();

    if (ensureOver.length == 0){
        
        let gameoverDiv = document.createElement('div');
        gameoverDiv.className = "gameover";

        gameoverDiv.innerHTML = "GAME OVER";
        
        let gameoverTryAgainText = document.createElement('p');
        gameoverTryAgainText.className = "gameover-contents"
        gameoverTryAgainText.innerHTML = "press enter to try again"
        gameoverDiv.appendChild(gameoverTryAgainText)
        
        wrapper.appendChild(gameoverDiv)
    }
}




//==========================PAUSE===============================
function pause(){
    game.state.is_running = false;
    displayScoreboard();

    let pauseDiv = document.createElement('div');
    pauseDiv.className = "pause";

    pauseDiv.innerHTML = "PAUSED";

    wrapper.appendChild(pauseDiv)
}

function unpause(){
    game.state.is_running = true;
    hideScoreboard();

    let pause = document.getElementsByClassName("pause")[0];

    if (pause) {
        wrapper.removeChild(pause)
    } else {
        wrapper.removeChild(startScreen)
    }
}
//=========================================================




function restartGame() {
    let history = game.state.pastscores;
    let historyStr = encodeURIComponent(JSON.stringify(history));

    let url = '//' + location.host + location.pathname
    url += "?history=" + historyStr;
    window.location.href = url
}




//==========================SCOREBOARD===============================
function displayScoreboard() {
    let scores = game.state.pastscores;
    let scoreboard = document.createElement('div');
    scoreboard.className = "scoreboard"
    scoreboard.innerHTML = "TOP 5 SCORES:"

    if (scores.length > 0) {
        let sortedScores = scores.sort().reverse();

        for (let index = 0; index < sortedScores.length; index++) {
            const i = sortedScores[index];
            let item = document.createElement('p');
            item.className = "scoreboard-contents"
            item.innerHTML = (index + 1).toString() + ". -- " + i.toString() + "p"
            scoreboard.appendChild(item)
            if (index >= 4){
                break;
            }
        }
    }

    wrapper.appendChild(scoreboard);
}

function hideScoreboard(){
    wrapper.removeChild(document.getElementsByClassName("scoreboard")[0])
}
//=========================================================
