
var MainApp = React.createClass({
    render: function() {
        return (
          <div>            
              <AppHeader />
              <AppMenu />
              <AppBody  />
              <AppFooter />
          </div>
      );
    }
});

var AppHeader = React.createClass({
    render: function () { 
        return(
            <div>
                This is Header
            </div>
        );
    }
});
var AppMenu = React.createClass({
    render: function () { 
        return(
            <div className="navbar">
                This is Menu
            </div>
        );
    }
});
var AppBody = React.createClass({
   
    render: function () {        
        return(
            <div className="row">
                <div className="col-lg-6">
                    <AppContent />
                </div>
                <div className="col-lg-6">
                    <AppSidebar />
                </div>
            </div>
        );
    }
});

var AppFooter = React.createClass({
    render: function () { 
        return (


            <div>
                This is Footer
            </div>
        );
    }
});

var AppContent = React.createClass({
    render: function () { 
        return (
            <div>
                    <AppOrderForm />
            </div>
        );
    }
});
var AppSidebar = React.createClass({
    render: function () { 
        return (


            <div>
                This is Sidebar
            </div>
        );
    }
});

var AppOrderForm = React.createClass({
   
    render: function () { 
        return(
            <form>                
                <input type="text" placeholder="Customer"  ref="customer" /><br/>
                <input type="submit" value="Add Order" />
            </form>
        );
    }
});


React.render(
  <MainApp data="Powerful Property" pollInterval={2000} />,
  document.getElementById('content')
);