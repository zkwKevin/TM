class CommentBox extends React.Component {
    render() {
      return (
        <div className="footer" >
          <p>Coming soon</p>
        </div>
      );
    }
  }
  
  ReactDOM.render(
    <CommentBox />,
    document.getElementById('content')
  );