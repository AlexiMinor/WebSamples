function writeComment() {
    console.log("writeComment");

    let commentModal = new bootstrap.Modal(document.getElementById('create-commend-modal'));
    commentModal.show(500);

    let confirmButton = document.getElementById('create-comment-btn');
    //
    //confirmButton.onclick = createComment; //createComment() => wrong 
    confirmButton.addEventListener('click', () => {
        createComment(commentModal);
    });

    //confirmButton.removeEventListener
}

function createComment(modal) {
    let author = document.getElementById('author-input').value;
    let content = document.getElementById('content-input').value;

    //const request = new XMLHttpRequest();
    //request.open("POST", "/comment/create", true);
    //request.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    //request.send({
    //    author: author,
    //    content: content
    //})

    const comment = {
        author: author,
        content: content
    };
    console.log(author, content);

    fetch('/comment/create', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json; charset=utf-8'
        },
        body: JSON.stringify(comment)
    })
        .then(response => {
            modal.hide(500);
            updateCommentSection();

        })
        .catch(response => console.log(response))
}

function updateCommentSection() {
    fetch('/comment/', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json; charset=utf-8'
        },
    })
        .then(response => response.json())
        .then(comments => renderCommentsSection(comments));
}

function renderCommentsSection(comments) {
    console.log(comments);
    if (comments.length >= 1) {
        let commentSection = document.getElementById('comment-section');
        commentSection.classList.remove('hidden');
        let commentsCollectionElement = commentSection.getElementsByClassName('comments')[0];
        commentsCollectionElement.innerHTML = "";
        for (let comment of comments) {
            let commentElement = document.createElement('div');
            commentElement.innerHTML = `<h5>${comment.author}</h5><p>${comment.content}</p><span>${comment.createdAt}</span>`;
            commentsCollectionElement.appendChild(commentElement);
        }
    }
}

window.addEventListener('DOMContentLoaded', () => updateCommentSection());

let data = await fetch()