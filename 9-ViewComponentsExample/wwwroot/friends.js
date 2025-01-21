document.querySelector("#load-friends").addEventListener('click', async function () {
    var response = await fetch('/friends-list', {
        method: 'GET',
    })

    var text = await response.text()
    document.querySelector('#friends-list').innerHTML = text
})