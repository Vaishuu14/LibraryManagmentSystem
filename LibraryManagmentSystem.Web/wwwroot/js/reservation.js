const reservationUri = 'https://localhost:7207/api/reservation';
let reservations = [];

async function getReservations() {
    try {
        const response = await fetch(`${reservationUri}/list`);
        const data = await response.json();
        reservations = data;
        _displayReservations(data);
    } catch (error) {
        console.error('Unable to get reservations.', error);
    }
}

async function addReservation() {
    const addBookIdTextbox = document.getElementById('add-bookId');
    const addBookTitleTextbox = document.getElementById('add-bookTitle');
    const addMemberIdTextbox = document.getElementById('add-memberId');
    const addMemberNameTextbox = document.getElementById('add-memberName');
    const addReservedDateTextbox = document.getElementById('add-reservedDate');
    const addExpirationDateTextbox = document.getElementById('add-expirationDate');

    const reservation = {
        bookId: parseInt(addBookIdTextbox.value.trim(), 10),
        bookTitle: addBookTitleTextbox.value.trim(),
        memberId: parseInt(addMemberIdTextbox.value.trim(), 10),
        memberName: addMemberNameTextbox.value.trim(),
        reservedDate: addReservedDateTextbox.value,
        expirationDate: addExpirationDateTextbox.value ? addExpirationDateTextbox.value : null
    };

    try {
        const response = await fetch(`${reservationUri}/create`, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(reservation)
        });

        if (response.ok) {
            getReservations();
            addBookIdTextbox.value = '';
            addBookTitleTextbox.value = '';
            addMemberIdTextbox.value = '';
            addMemberNameTextbox.value = '';
            addReservedDateTextbox.value = '';
            addExpirationDateTextbox.value = '';
        } else {
            console.error('Unable to add reservation.');
            const errorText = await response.text();
            console.error(errorText);
        }
    } catch (error) {
        console.error('Unable to add reservation.', error);
    }
}

async function deleteReservation(id) {
    try {
        const response = await fetch(`${reservationUri}/delete/${id}`, {
            method: 'DELETE'
        });

        if (response.ok) {
            getReservations();
        } else {
            console.error('Unable to delete reservation.');
        }
    } catch (error) {
        console.error('Unable to delete reservation.', error);
    }
}

function displayEditForm(id) {
    const reservation = reservations.find(reservation => reservation.id === id);

    document.getElementById('edit-id').value = reservation.id;
    document.getElementById('edit-bookId').value = reservation.bookId;
    document.getElementById('edit-bookTitle').value = reservation.bookTitle;
    document.getElementById('edit-memberId').value = reservation.memberId;
    document.getElementById('edit-memberName').value = reservation.memberName;
    document.getElementById('edit-reservedDate').value = new Date(reservation.reservedDate).toISOString().split('T')[0];
    document.getElementById('edit-expirationDate').value = reservation.expirationDate ? new Date(reservation.expirationDate).toISOString().split('T')[0] : '';
    document.getElementById('editForm').style.display = 'block';
}

async function updateReservation() {
    const reservationId = document.getElementById('edit-id').value;
    const bookId = document.getElementById('edit-bookId').value.trim();
    const bookTitle = document.getElementById('edit-bookTitle').value.trim();
    const memberId = document.getElementById('edit-memberId').value.trim();
    const memberName = document.getElementById('edit-memberName').value.trim();
    const reservedDate = document.getElementById('edit-reservedDate').value;
    const expirationDate = document.getElementById('edit-expirationDate').value;

    const reservation = {
        id: parseInt(reservationId, 10),
        bookId: parseInt(bookId, 10),
        bookTitle: bookTitle,
        memberId: parseInt(memberId, 10),
        memberName: memberName,
        reservedDate: new Date(reservedDate).toISOString(),
        expirationDate: expirationDate ? new Date(expirationDate).toISOString() : null
    };

    try {
        const response = await fetch(`${reservationUri}/update`, {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(reservation)
        });

        if (response.ok) {
            getReservations();
            closeInput();
        } else {
            console.error('Unable to update reservation.');
        }
    } catch (error) {
        console.error('Unable to update reservation.', error);
    }
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displayReservations(data) {
    const tBody = document.getElementById('reservations');
    tBody.innerHTML = '';

    data.forEach(reservation => {
        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let bookTitleTextNode = document.createTextNode(reservation.bookTitle);
        td1.appendChild(bookTitleTextNode);

        let td2 = tr.insertCell(1);
        let memberNameTextNode = document.createTextNode(reservation.memberName);
        td2.appendChild(memberNameTextNode);

        let td3 = tr.insertCell(2);
        let reservedDateTextNode = document.createTextNode(new Date(reservation.reservedDate).toLocaleDateString());
        td3.appendChild(reservedDateTextNode);

        let td4 = tr.insertCell(3);
        let expirationDateTextNode = document.createTextNode(reservation.expirationDate ? new Date(reservation.expirationDate).toLocaleDateString() : '');
        td4.appendChild(expirationDateTextNode);

        let td5 = tr.insertCell(4);
        let editButton = document.createElement('button');
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${reservation.id})`);
        td5.appendChild(editButton);

        let td6 = tr.insertCell(5);
        let deleteButton = document.createElement('button');
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteReservation(${reservation.id})`);
        td6.appendChild(deleteButton);
    });

    const counter = document.getElementById('counter');
    const name = data.length === 1 ? 'reservation' : 'reservations';
    counter.innerText = `${data.length} ${name}`;
}

getReservations();
