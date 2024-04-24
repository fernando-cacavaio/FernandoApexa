import { Button } from "semantic-ui-react";
import { AdvisorDto } from "../../models/advisorDto";
import apiConnector from "../../api/apiConnector";
import { NavLink } from "react-router-dom";

interface Props {
  advisor: AdvisorDto;
}

export default function AdvisorTableItem({ advisor }: Props) {
  function addDash(text: string) {
    let finalVal = text == null ? "" : text.replace(/(\d{4})/, "$1-");
    return finalVal;
  }

  function numberWithSpaces(text: string) {
    return text.replace(/\B(?=(\d{3})+(?!\d))/g, " ");
  }

  return (
    <>
      <tr className="center aligned">
        <td data-label="Name">{advisor.name}</td>
        <td data-label="SIN">{numberWithSpaces(advisor.sin)}</td>
        <td data-label="Phone">{advisor.address}</td>
        <td data-label="Address">{addDash(advisor.phone)}</td>
        <td data-label="Status">{advisor.healthStatus}</td>
        <td data-label="Action">
          <Button
            as={NavLink}
            to={`editAdvisor/${advisor.id}`}
            color="yellow"
            type="submit"
          >
            Edit
          </Button>
          <Button
            type="button"
            negative
            onClick={async () => {
              await apiConnector.deleteAvisor(advisor.id!);
              window.location.reload();
            }}
          >
            Delete
          </Button>
        </td>
      </tr>
    </>
  );
}
