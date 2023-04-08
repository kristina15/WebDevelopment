var $form = $('#newLoan'),
    $loanList = $('#loanList'),
    $addLoan = $("#addLoan"),
    $roleList = $("#roleList");

$form.submit(() => {
    var formData = $form.serialize();

    $.ajax({
        url: CFG.saveLoansUrl,
        type: 'post',
        data: formData
    })
        .done((result) => {
            location.reload(result);
        });

    return false;
});

$addLoan.click(() => {
    var formData = $form.serialize();

    $.ajax({
        url: CFG.createLoanUrl,
        type: 'post',
        data: formData
    })
        .done((data) => {
            $loanList.append(data);
        });

    return false;
})

function DeleteLoan(id) {
    if (confirm("Are you sure you want to delete this loan?")) {
        var row = $(this).parent().parent().children().index($(this).parent());
        document.getElementById("myTable").deleteRow(row);
        $.ajax({
            url: CFG.deleteUrl + '/' + id,
            type: 'post'
        })
    }
};

function DeleteNewLoan(sum) {
    var row = $(this).parent().parent().children().index($(this).parent());
    document.getElementById("myTable").deleteRow(row);

    $.ajax({
        url: CFG.deleteNewLoanUrl + '?sum=' + sum,
        type: 'post'
    })
}

function UpdateStatus(id) {
    var row = $(this).parent().parent().children().index($(this).parent());
    document.getElementById("loanTable").deleteRow(row);
    var status = document.getElementById("statuses").value;
    $.ajax({
        url: CFG.updateStatus + '?id=' + id + '&status=' + status,
        type: 'post'
    })
}

function AddRole(login) {
    var role = document.getElementById("roles").value,
        tds = document.querySelectorAll("td"),
        index;

    for (var i = 3; i < tds.length; i += 6) {
        let log = tds[i].firstChild.nodeValue.trim();
        if (log.localeCompare(login) == 0) {
            index = i;
            break;
        }
    }

    $.ajax({
        url: CFG.addRole + '?login=' + login + '&role=' + role,
        type: 'post'
    })
        .done((data) => {
            tds[index + 1].append(data);
        })
        .fail(() => {
            alert("Such role alredy exists")
        })
}

