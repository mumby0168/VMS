import { ListItem, ListItemText, ListItemIcon } from '@material-ui/core'
import React from 'react'
import { NavLink } from 'react-router-dom';

export default function ListItemLink(props) {
    const { icon, primary, to, onClick } = props;
  
    const renderLink = React.useMemo(
      () => React.forwardRef((itemProps, ref) => <NavLink to={to} ref={ref} {...itemProps} />),
      [to],
    );
  
    return (
      <li onClick={onClick}>
        <ListItem button component={renderLink}>
          {icon ? <ListItemIcon>{icon}</ListItemIcon> : null}
          <ListItemText primary={primary} />
        </ListItem>
      </li>
    );
  }