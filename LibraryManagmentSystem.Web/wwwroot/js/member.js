const uri = 'https://localhost:7207/api/member';
let members = [];

async function getMembers() {
    try {
        debugger;
        const response = await fetch(`${uri}/list`);
        const data = await response.json();
        members = data;
        _displayMembers(data);
    } catch (error) {
        console.error('Unable to get members.', error);
    }
}

async function addMember() {
    const addFirstNameTextbox = document.getElementById('add-firstName');
    const addLastNameTextbox = document.getElementById('add-lastName');
    const addEmailTextbox = document.getElementById('add-email');
    const addContactNumberTextbox = document.getElementById('add-contactNumber');
    const addDateOfMembershipTextbox = document.getElementById('add-dateOfMembership');

    const member = {
        firstName: addFirstNameTextbox.value.trim(),
        lastName: addLastNameTextbox.value.trim(),
        email: addEmailTextbox.value.trim(),
        contactNumber: addContactNumberTextbox.value.trim(),
        dateOfMembership: addDateOfMembershipTextbox.value
    };

    try {
        const response = await fetch(`${uri}/create`, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(member)
        });

        if (response.ok) {
            getMembers();
            addFirstNameTextbox.value = '';
            addLastNameTextbox.value = '';
            addEmailTextbox.value = '';
            addContactNumberTextbox.value = '';
            addDateOfMembershipTextbox.value = '';
        } else {
            console.error('Unable to add member.');
            const errorText = await response.text();
            console.error(errorText);
        }
    } catch (error) {
        console.error('Unable to add member.', error);
    }
}

async function deleteMember(id) {
    try {
        const response = await fetch(`${uri}/delete/${id}`, {
            method: 'DELETE'
        });

        if (response.ok) {
            getMembers();
        } else {
            console.error('Unable to delete member.');
        }
    } catch (error) {
        console.error('Unable to delete member.', error);
    }
}

function displayEditForm(id) {
    const member = members.find(member => member.id === id);

    document.getElementById('edit-id').value = member.id;
    document.getElementById('edit-firstName').value = member.firstName;
    document.getElementById('edit-lastName').value = member.lastName;
    document.getElementById('edit-email').value = member.email;
    document.getElementById('edit-contactNumber').value = member.contactNumber;
    document.getElementById('edit-dateOfMembership').value = new Date(member.dateOfMembership).toISOString().split('T')[0];
    document.getElementById('editForm').style.display = 'block';
}

async function updateMember() {
    const memberId = document.getElementById('edit-id').value;
    const memberFirstName = document.getElementById('edit-firstName').value.trim();
    const memberLastName = document.getElementById('edit-lastName').value.trim();
    const memberEmail = document.getElementById('edit-email').value.trim();
    const memberContactNumber = document.getElementById('edit-contactNumber').value.trim();
    const memberDateOfMembership = document.getElementById('edit-dateOfMembership').value;

    const member = {
        id: parseInt(memberId, 10),
        firstName: memberFirstName,
        lastName: memberLastName,
        email: memberEmail,
        contactNumber: memberContactNumber,
        dateOfMembership: new Date(memberDateOfMembership).toISOString()
    };

    try {
        const response = await fetch(`${uri}/update`, {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(member)
        });

        if (response.ok) {
            getMembers();
            closeInput();
        } else {
            console.error('Unable to update member.');
        }
    } catch (error) {
        console.error('Unable to update member.', error);
    }
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displayMembers(data) {
    const tBody = document.getElementById('members');
    tBody.innerHTML = '';

    data.forEach(member => {
        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let firstNameTextNode = document.createTextNode(member.firstName);
        td1.appendChild(firstNameTextNode);

        let td2 = tr.insertCell(1);
        let lastNameTextNode = document.createTextNode(member.lastName);
        td2.appendChild(lastNameTextNode);

        let td3 = tr.insertCell(2);
        let emailTextNode = document.createTextNode(member.email);
        td3.appendChild(emailTextNode);

        let td4 = tr.insertCell(3);
        let contactNumberTextNode = document.createTextNode(member.contactNumber);
        td4.appendChild(contactNumberTextNode);

        let td5 = tr.insertCell(4);
        let dateOfMembershipTextNode = document.createTextNode(new Date(member.dateOfMembership).toLocaleDateString());
        td5.appendChild(dateOfMembershipTextNode);

        let td6 = tr.insertCell(5);
        let editButton = document.createElement('button');
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${member.id})`);
        td6.appendChild(editButton);

        let td7 = tr.insertCell(6);
        let deleteButton = document.createElement('button');
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteMember(${member.id})`);
        td7.appendChild(deleteButton);
    });

    const counter = document.getElementById('counter');
    const name = data.length === 1 ? 'member' : 'members';
    counter.innerText = `${data.length} ${name}`;
}

getMembers();
