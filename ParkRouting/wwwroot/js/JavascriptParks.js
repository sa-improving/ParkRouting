const query = document.querySelector("#query");
const parksContainer = document.querySelector("#parksContainer");

function produceParks(data) {

    data.forEach(element => {
        const br = document.createElement("br");
        const park = document.createElement("div");

        const parkName = document.createElement("h3");
        parkName.textContent = element.parkname;

        const parkBorough = document.createElement("p");
        parkBorough.textContent = element.borough;

        const parkAcres = document.createElement("p");
        parkAcres.textContent = "Acres: " + element.acres;

        const parkDescription = document.createElement("p");
        parkDescription.innerHTML = element.description;

        parksContainer.appendChild(br);
        parksContainer.appendChild(park);
        parksContainer.appendChild(parkName);
        parksContainer.appendChild(parkBorough);
        parksContainer.appendChild(parkAcres);
        parksContainer.appendChild(parkDescription);
    })
}

function parkFetch(input) {
    let endpointBaseUrl = "/javascriptjson?search=" + input;
    fetch(endpointBaseUrl)
        .then((response) => {
            return response.json()
        })
        .then((data) => {
            console.log(data);
            produceParks(data);
        });
}

query.addEventListener("keyup", () => {
    parksContainer.textContent = "";
    let input = query.value.trim();
    parkFetch(input);
})