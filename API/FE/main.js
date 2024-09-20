$(document).ready(function () {
    // Load flowers
    $('#load-flowers-btn').click(loadFlowers);

    // Add or update flower
    $('#flower-form').submit(function (event) {
        event.preventDefault();
        const flowerId = $('#flower-id').val();
        const flowerData = {
            name: $('#flower-name').val(),
            color: $('#flower-color').val(),
            type: $('#flower-type').val(),
            price: parseFloat($('#flower-price').val()),
            stockQuantity: parseInt($('#flower-stock').val())
        };

        if (flowerId) {
            updateFlower(flowerId, flowerData);
        } else {
            addFlower(flowerData);
        }
    });

    // Load flowers on page load
    loadFlowers();
});

// Load flowers function
function loadFlowers() {
    $.ajax({
        url: 'https://localhost:7108/api/flowers',
        method: 'GET',
        success: function (data) {
            const flowerTableBody = $('#flowerTable tbody');
            flowerTableBody.empty(); // Clear previous rows
            data.forEach(flower => {
                flowerTableBody.append(`
                    <tr>
                        <td>${flower.flowerId}</td>
                        <td>${flower.name}</td>
                        <td>${flower.color}</td>
                        <td>${flower.type}</td>
                        <td>${flower.price}</td>
                        <td>${flower.stockQuantity}</td>
                        <td>
                            <button class="edit-flower" data-id="${flower.flowerId}">Edit</button>
                            <button class="delete-flower" data-id="${flower.flowerId}">Delete</button>
                        </td>
                    </tr>
                `);
            });

            // Attach edit and delete events
            $('.edit-flower').click(function () {
                const flowerId = $(this).data('id');
                editFlower(flowerId);
            });
            $('.delete-flower').click(function () {
                const flowerId = $(this).data('id');
                deleteFlower(flowerId);
            });
        },
        error: function () {
            showMessage("Failed to load flowers.", "error");
        }
    });
}

// Add flower function
function addFlower(flowerData) {
    $.ajax({
        url: 'https://localhost:7108/api/flowers',
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(flowerData),
        success: function () {
            showMessage("Flower added successfully!");
            loadFlowers();
            $('#flower-form')[0].reset();
        },
        error: function () {
            showMessage("Failed to add flower.", "error");
        }
    });
}

// Update flower function
function updateFlower(flowerId, flowerData) {
    $.ajax({
        url: `https://localhost:7108/api/flowers/${flowerId}`,
        method: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify(flowerData),
        success: function () {
            showMessage("Flower updated successfully!");
            loadFlowers();
            $('#flower-form')[0].reset();
            $('#flower-id').val('');
            $('#add-flower-btn').show();
            $('#update-flower-btn').hide();
        },
        error: function () {
            showMessage("Failed to update flower.", "error");
        }
    });
}

// Edit flower function
function editFlower(flowerId) {
    $.ajax({
        url: `https://localhost:7108/api/flowers/${flowerId}`,
        method: 'GET',
        success: function (flower) {
            $('#flower-id').val(flower.flowerId);
            $('#flower-name').val(flower.name);
            $('#flower-color').val(flower.color);
            $('#flower-type').val(flower.type);
            $('#flower-price').val(flower.price);
            $('#flower-stock').val(flower.stockQuantity);
            $('#add-flower-btn').hide();
            $('#update-flower-btn').show();
        },
        error: function () {
            showMessage("Failed to load flower details.", "error");
        }
    });
}

// Delete flower function
function deleteFlower(flowerId) {
    $.ajax({
        url: `https://localhost:7108/api/flowers/${flowerId}`,
        method: 'DELETE',
        success: function () {
            showMessage("Flower deleted successfully!");
            loadFlowers();
        },
        error: function () {
            showMessage("Failed to delete flower.", "error");
        }
    });
}

// Show message function
function showMessage(message, type = "success") {
    const messageDiv = $('#message');
    messageDiv.text(message);
    messageDiv.removeClass("error").addClass(type);
    messageDiv.show();
    setTimeout(() => {
        messageDiv.hide();
    }, 3000);
}
