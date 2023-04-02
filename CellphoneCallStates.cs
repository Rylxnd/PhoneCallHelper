namespace PhoneCallHelper
{
    internal enum eCellphoneCallState : int
    {
        Dialing,
        Incoming,
        Connected
    }

    internal static class CellphoneCallState
    {
        public static string GetCellphoneStateString(eCellphoneCallState state)
        {
            switch (state)
            {
                case eCellphoneCallState.Dialing:
                    return "CELL_211";
                case eCellphoneCallState.Incoming:
                    return "CELL_217";
                case eCellphoneCallState.Connected:
                    return "CELL_219";
            }

            return string.Empty;
        }
    }
}
