﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ITLlib;
using Kiosko.Library.CashPayment.SmartHopper.SSP;

using System.Text;
using static Kiosko.Library.CashPayment.SmartHopper.PollResponse;
using Prism.Events;
using Kiosko.Events;
using Kiosko.Models;
using System.Windows.Forms;

namespace Kiosko.Library.CashPayment.SmartHopper
{
    public class CHopper
    {
        // handles to SSP classes
        SSPComms m_eSSP;
        SSP_COMMAND m_cmd;
        SSP_KEYS keys;
        SSP_FULL_KEY sspKey;
        SSP_COMMAND_INFO info;

        // variable declarations

        // The protocol version this hopper is using, set in setup request
        int m_ProtocolVersion;

        // The number of channels being used in this dataset.
        int m_NumberOfChannels;

        // A boolean indicating whether the coin mech is enabled or disabled (globally inhibited).
        bool m_CoinMechEnabled;



        // A variable to hold the type of the unit, obtained in setup request
        char m_UnitType;

        // A list of dataset data, sorted by value. Holds the info on channel number, value, currency,
        // level and whether it is being recycled.
        List<ChannelData> m_UnitDataList;

        IEventAggregator _evnt;

        InventarioEfectivo _inventory;

        // constructor
        public CHopper(IEventAggregator _ea, InventarioEfectivo Inv)
        {

            _inventory = Inv;

            _evnt = _ea;
            // init SSP handles
            m_eSSP = new SSPComms();
            m_cmd = new SSP_COMMAND();
            keys = new SSP_KEYS();
            sspKey = new SSP_FULL_KEY();
            info = new SSP_COMMAND_INFO();

            m_NumberOfChannels = 0;
            m_ProtocolVersion = 0;
            m_CoinMechEnabled = true;

            m_UnitDataList = new List<ChannelData>();

            //if (Properties.Settings.Default.Comms)
            //    Comms.Show();
        }

        /* Variable Access */



        // access to number of channels
        public int NumberOfChannels
        {
            get { return m_NumberOfChannels; }
            set { m_NumberOfChannels = value; }
        }

        // access to coin mech bool
        public bool CoinMechEnabled
        {
            get { return m_CoinMechEnabled; }
            set { m_CoinMechEnabled = value; }
        }

        // access to the command structure
        public SSP_COMMAND CommandStructure
        {
            get { return m_cmd; }
            set { m_cmd = value; }
        }

        // access to the info structure
        //public SSP_COMMAND_INFO InfoStructure
        //{
        //    get { return info; }
        //    set { info = value; }
        //}

        // access to the comms structure
        public SSPComms SSPComms
        {
            get { return m_eSSP; }
            set { m_eSSP = value; }
        }

        // access to the unit's type
        public char UnitType
        {
            get { return m_UnitType; }
        }

        // access to the dataset data
        public List<ChannelData> UnitDataList
        {
            get { return m_UnitDataList; }
        }

        // get a channel value
        public int GetChannelValue(int channelNum)
        {
            if (channelNum > 0 && channelNum <= m_NumberOfChannels)
            {
                foreach (ChannelData d in m_UnitDataList)
                {
                    if (d.Channel == channelNum)
                        return d.Value;
                }
            }
            return -1;
        }

        // Command functions

        // This function send the SYNC command to the hopper. It returns true if it receives an OK response.
        public bool SendSync(log4net.ILog log = null)
        {
            m_cmd.CommandData[0] = CCommands.SSP_CMD_SYNC;
            m_cmd.CommandDataLength = 1;
            if (!SendCommand(log)) return false;
            if (CheckGenericResponses(log))
                return true;
            return false;
        }

        // The enable command allows the hopper to receive and act on commands.
        public void EnableHopper(log4net.ILog log = null)
        {

            m_cmd.CommandData[0] = CCommands.SSP_CMD_ENABLE;
            m_cmd.CommandDataLength = 1;

            if (!SendCommand(log)) return;
            // check response
            if (CheckGenericResponses(log))
            {
                if (log != null)
                    log.Debug("SMART Hopper enabled\r\n");
            }
        }

        // This function sends the Set Hopper Options command to the hopper.  
        public void SetHopperOptions(byte payMode, byte levelCheck, byte motorSpeed, byte cashboxPayActive, log4net.ILog log = null)
        {
            byte OptionData = 0x00;
            m_cmd.CommandData[0] = CCommands.SSP_CMD_SET_HOPPER_OPTIONS;

            // Options are sent as single bits.  Make up byte from parameter values. 
            if (payMode != 0x00) OptionData += 0x01;
            if (levelCheck != 0x00) OptionData += 0x02;
            if (motorSpeed != 0x00) OptionData += 0x04;
            if (cashboxPayActive != 0x00) OptionData += 0x08;

            m_cmd.CommandData[1] = OptionData;
            // second byte of data is not used.
            m_cmd.CommandData[2] = 0x00;
            m_cmd.CommandDataLength = 3;

            if (!SendCommand(log)) return;
            // check response
            if (CheckGenericResponses(log))
            {
                if (log != null)
                    log.Debug("\r\nSMART Hopper options set\r\n");
            }

        }

        // This funtion sends the Get Hopper Options command and displays the results.
        public void GetHopperOptions(log4net.ILog log = null)
        {
            m_cmd.CommandData[0] = CCommands.SSP_CMD_GET_HOPPER_OPTIONS;
            m_cmd.CommandDataLength = 1;

            if (!SendCommand(log)) return;

            // Options carried as single bits, so decode.
            log.Debug("Hopper Options\r\n");
            if ((m_cmd.ResponseData[1] & 0x01) == 0x01)
            {
                log.Debug("Pay Mode: Free Pay\r\n");
            }
            else
            {
                log.Debug("Pay Mode: Highest value\r\n");
            }
            if ((m_cmd.ResponseData[1] & 0x02) == 0x02)
            {
                log.Debug("Level Check: Enabled\r\n");
            }
            else
            {
                log.Debug("Level Check: Disabled\r\n");
            }
            if ((m_cmd.ResponseData[1] & 0x04) == 0x04)
            {
                log.Debug("Motor Speed: High\r\n");
            }
            else
            {
                log.Debug("Motor Speed: Low\r\n");
            }
            if ((m_cmd.ResponseData[1] & 0x08) == 0x08)
            {
                log.Debug("Cashbox Pay: Active\r\n");
            }
            else
            {
                log.Debug("Cashbox Pay: Inactive\r\n");
            }
        }


        // Empty device moves all the coins in the device to the cashbox using command EMPTY ALL. It then
        // sets the channel levels to 0.
        public void EmptyDevice(log4net.ILog log = null)
        {
            m_cmd.CommandData[0] = CCommands.SSP_CMD_EMPTY_ALL;
            m_cmd.CommandDataLength = 1;

            if (!SendCommand(log)) return;
            if (CheckGenericResponses(log) && log != null)
            {
                log.Debug("Emptying all stored coins to cashbox...\r\n");
                foreach (ChannelData d in m_UnitDataList)
                    d.Level = 0;
            }
        }

        // Set a channel to route to cashbox, this sends the SET ROUTING command.
        public void RouteChannelToCashbox(int channelNumber, log4net.ILog log = null)
        {
            // setup command
            m_cmd.CommandData[0] = CCommands.SSP_CMD_SET_DENOMINATION_ROUTE;
            m_cmd.CommandData[1] = 0x01; // cashbox

            // coin to route

            // Get value of coin (4 byte protocol 6)
            byte[] b = CHelpers.ConvertInt32ToBytes(GetChannelValue(channelNumber));
            m_cmd.CommandData[2] = b[0];
            m_cmd.CommandData[3] = b[1];
            m_cmd.CommandData[4] = b[2];
            m_cmd.CommandData[5] = b[3];

            // Add country code, locate from dataset
            foreach (ChannelData d in m_UnitDataList)
            {
                if (d.Channel == channelNumber)
                {
                    m_cmd.CommandData[6] = (byte)d.Currency[0];
                    m_cmd.CommandData[7] = (byte)d.Currency[1];
                    m_cmd.CommandData[8] = (byte)d.Currency[2];
                    break;
                }
            }

            m_cmd.CommandDataLength = 9;

            // send command
            if (!SendCommand(log)) return;

            if (CheckGenericResponses(log))
            {
                // update list
                foreach (ChannelData d in m_UnitDataList)
                {
                    if (d.Channel == channelNumber)
                    {
                        d.Recycling = false;
                        break;
                    }
                }

                if (log != null)
                    log.Debug("Successfully routed coin on channel " + channelNumber.ToString() + " to cashbox\r\n");
            }
        }
        // Set a channel to route to storage, this sends the SET ROUTING command.
        public void RouteChannelToStorage(int channelNumber, log4net.ILog log = null)
        {
            // setup command
            m_cmd.CommandData[0] = CCommands.SSP_CMD_SET_DENOMINATION_ROUTE;
            m_cmd.CommandData[1] = 0x00; // storage

            // coin to route

            // Get value of coin (4 byte protocol 6)
            byte[] b = CHelpers.ConvertInt32ToBytes(GetChannelValue(channelNumber));
            m_cmd.CommandData[2] = b[0];
            m_cmd.CommandData[3] = b[1];
            m_cmd.CommandData[4] = b[2];
            m_cmd.CommandData[5] = b[3];

            // Add country code, locate from dataset
            foreach (ChannelData d in m_UnitDataList)
            {
                if (d.Channel == channelNumber)
                {
                    m_cmd.CommandData[6] = (byte)d.Currency[0];
                    m_cmd.CommandData[7] = (byte)d.Currency[1];
                    m_cmd.CommandData[8] = (byte)d.Currency[2];
                    break;
                }
            }

            m_cmd.CommandDataLength = 9;

            // send command
            if (!SendCommand(log)) return;

            if (CheckGenericResponses(log))
            {
                // update list
                foreach (ChannelData d in m_UnitDataList)
                {
                    if (d.Channel == channelNumber)
                    {
                        d.Recycling = true;
                        break;
                    }
                }

                if (log != null)
                    log.Debug("Successfully routed coin on channel " + channelNumber.ToString() + " to storage\r\n");
            }
        }

        // Disable command stops the unit accepting commands and acting on them.
        public void DisableHopper(log4net.ILog log = null)
        {
            m_cmd.CommandData[0] = CCommands.SSP_CMD_DISABLE;
            m_cmd.CommandDataLength = 1;

            if (!SendCommand(log)) return;
            // check response
            if (CheckGenericResponses(log) && log != null)
                log.Debug("SMART Hopper disabled\r\n");
        }

        // The reset command instructs the hopper to restart (same effect as switching on and off)
        public void Reset(log4net.ILog log = null)
        {
            m_cmd.CommandData[0] = CCommands.SSP_CMD_RESET;
            m_cmd.CommandDataLength = 1;

            if (!SendCommand(log)) return;
            CheckGenericResponses(log);
        }

        // This uses the PAYOUT AMOUNT command to payout a value specified by the param amountToPayout.
        // Protocol 6+ - We can use an option byte to test whether the payout is possible (0x19), and if
        // it is then we can resend with the option byte 0x58 to do the payout.
        public bool PayoutAmount(int amountToPayout, char[] currency, log4net.ILog log = null)
        {
            m_cmd.CommandData[0] = CCommands.SSP_CMD_PAYOUT_AMOUNT;

            // Value to payout
            byte[] b = CHelpers.ConvertInt32ToBytes(amountToPayout);
            m_cmd.CommandData[1] = b[0];
            m_cmd.CommandData[2] = b[1];
            m_cmd.CommandData[3] = b[2];
            m_cmd.CommandData[4] = b[3];

            // Country code
            m_cmd.CommandData[5] = (byte)currency[0];
            m_cmd.CommandData[6] = (byte)currency[1];
            m_cmd.CommandData[7] = (byte)currency[2];

            m_cmd.CommandData[8] = 0x58; // payout option (0x19 for test, 0x58 for real)

            m_cmd.CommandDataLength = 9;

            if (!SendCommand(log)) return false;

            if (CheckGenericResponses(log))
                return true;
            return false;
        }

        // Payout by denomination. This function allows a developer to payout specified amounts of certain
        // coins. Due to the variable length of the data that could be passed to the function, the user 
        // passes an array containing the data to payout and the length of that array along with the number
        // of denominations they are paying out.
        public void PayoutByDenomination(byte numDenoms, byte[] data, byte dataLength, log4net.ILog log = null)
        {
            // First is the command byte
            m_cmd.CommandData[0] = CCommands.SSP_CMD_PAYOUT_BY_DENOMINATION;

            // Next is the number of denominations to be paid out
            m_cmd.CommandData[1] = numDenoms;

            // Copy over data byte array parameter into command structure
            int currentIndex = 2;
            for (int i = 0; i < dataLength; i++)
                m_cmd.CommandData[currentIndex++] = data[i];

            // Perform a real payout (0x19 for test)
            m_cmd.CommandData[currentIndex++] = 0x58;

            // Length of command data (add 3 to cover the command byte, num of denoms and real/test byte)
            dataLength += 3;
            m_cmd.CommandDataLength = dataLength;

            if (!SendCommand(log)) return;
            if (CheckGenericResponses(log))
            {
                if (log != null)
                    log.Debug("Paying out by denomination...\r\n");
            }
        }

        // This function uses the COIN MECH GLOBAL INHIBIT command to disable the coin mech.
        public bool DisableCoinMech(log4net.ILog log = null)
        {
            m_cmd.CommandData[0] = CCommands.SSP_CMD_SET_COIN_MECH_GLOBAL_INHIBIT;
            m_cmd.CommandData[1] = 0x00; // 0 for disable
            m_cmd.CommandDataLength = 2;

            if (!SendCommand(log)) return false;
            if (CheckGenericResponses(log) && log != null)
            {
                log.Debug("Disabled coin mech\r\n");
                m_CoinMechEnabled = false;
                return true;
            }
            return false;
        }

        // This function uses the COIN MECH GLOBAL INHIBIT command to enable the coin mech.
        public bool EnableCoinMech(log4net.ILog log = null)
        {
            m_cmd.CommandData[0] = CCommands.SSP_CMD_SET_COIN_MECH_GLOBAL_INHIBIT;
            m_cmd.CommandData[1] = 0x01; // 1 for enable
            m_cmd.CommandDataLength = 2;

            if (!SendCommand(log)) return false;
            if (CheckGenericResponses(log))
            {
                m_CoinMechEnabled = true;
                //log.Debug("Enabled coin mech\r\n");
                return true;
            }
            return false;
        }

        // This function uses the command SMARTY EMPTY which empties all the coins to the cashbox but keeps a 
        // count of what was put in, the data of what coins were emptied can be accessed with the command
        // CASHBOX PAYOUT OPERATION DATA
        public void SmartEmpty(log4net.ILog log = null)
        {
            m_cmd.CommandData[0] = CCommands.SSP_CMD_SMART_EMPTY;
            m_cmd.CommandDataLength = 1;

            if (!SendCommand(log)) return;
            if (CheckGenericResponses(log))
            {
                if (log != null) log.Debug("SMART empyting...\r\n");
            }
        }

        // This uses the SET COIN AMOUNT command to increase a channel level by passing over the channel and the amount to increment by
        public void SetCoinLevelsByChannel(int channel, short amount, log4net.ILog log = null)
        {
            m_cmd.CommandData[0] = CCommands.SSP_CMD_SET_DENOMINATION_LEVEL;
            // Level to set
            byte[] b = CHelpers.ConvertInt16ToBytes(amount);
            m_cmd.CommandData[1] = b[0];
            m_cmd.CommandData[2] = b[1];

            // Coin(channel) to set
            b = CHelpers.ConvertInt32ToBytes(GetChannelValue(channel));
            m_cmd.CommandData[3] = b[0];
            m_cmd.CommandData[4] = b[1];
            m_cmd.CommandData[5] = b[2];
            m_cmd.CommandData[6] = b[3];

            // Add country code, locate from dataset
            foreach (ChannelData d in m_UnitDataList)
            {
                if (d.Channel == channel)
                {
                    m_cmd.CommandData[7] = (byte)d.Currency[0];
                    m_cmd.CommandData[8] = (byte)d.Currency[1];
                    m_cmd.CommandData[9] = (byte)d.Currency[2];
                    break;
                }
            }

            m_cmd.CommandDataLength = 10;

            if (!SendCommand(log)) return;
            if (CheckGenericResponses(log))
            {
                // Update the level
                foreach (ChannelData d in m_UnitDataList)
                {
                    if (d.Channel == channel)
                    {
                        d.Level += amount;
                        break;
                    }
                }

                if (log != null)
                {
                    log.Debug("Changed coin value " + CHelpers.FormatToCurrency(GetChannelValue(channel)).ToString() +
                        "'s level to " + amount.ToString() + "\r\n");
                }
            }

        }

        // This uses the SET COIN AMOUNT command to increase a channel level by passing over the coin value and the amount to increment by
        public void SetCoinLevelsByCoin(int coin, char[] currency, short amount, log4net.ILog log = null)
        {
            m_cmd.CommandData[0] = CCommands.SSP_CMD_SET_DENOMINATION_LEVEL;
            byte[] b = CHelpers.ConvertInt16ToBytes(amount);
            m_cmd.CommandData[1] = b[0];
            m_cmd.CommandData[2] = b[1];
            b = CHelpers.ConvertInt32ToBytes(coin);
            m_cmd.CommandData[3] = b[0];
            m_cmd.CommandData[4] = b[1];
            m_cmd.CommandData[5] = b[2];
            m_cmd.CommandData[6] = b[3];
            m_cmd.CommandData[7] = (byte)currency[0];
            m_cmd.CommandData[8] = (byte)currency[1];
            m_cmd.CommandData[9] = (byte)currency[2];
            m_cmd.CommandDataLength = 10;

            if (!SendCommand(log)) return;
            if (CheckGenericResponses(log))
            {
                // Update the level
                foreach (ChannelData d in m_UnitDataList)
                {
                    if (d.Value == coin)
                    {
                        d.Level += amount;
                        break;
                    }
                }

                if (log != null)
                    log.Debug("Increased coin value " + CHelpers.FormatToCurrency(coin).ToString() + "'s level by " + amount.ToString() + "\r\n");

            }
        }


        // Calls SetCoinLevelByChannel on all of the active channels.
        public void SetAllCoinLevels(short amount, log4net.ILog log = null)
        {
            foreach (ChannelData d in m_UnitDataList)
            {
                SetCoinLevelsByChannel(d.Channel, amount, log);
            }
        }


        // This uses the GET DENOMINATION LEVEL command to query the hoopper on a specified coin it has stored, it returns
        // the level as an int.

        public short CheckCoinLevel(int coinValue, char[] currency, log4net.ILog log = null)
        {
            m_cmd.CommandData[0] = CCommands.SSP_CMD_GET_DENOMINATION_LEVEL;
            byte[] b = CHelpers.ConvertInt32ToBytes(coinValue);
            m_cmd.CommandData[1] = b[0];
            m_cmd.CommandData[2] = b[1];
            m_cmd.CommandData[3] = b[2];
            m_cmd.CommandData[4] = b[3];
            m_cmd.CommandData[5] = (byte)currency[0];
            m_cmd.CommandData[6] = (byte)currency[1];
            m_cmd.CommandData[7] = (byte)currency[2];
            m_cmd.CommandDataLength = 8;

            if (!SendCommand(log)) return -1;
            if (CheckGenericResponses(log))
            {
                return CHelpers.ConvertBytesToInt16(m_cmd.ResponseData, 1);
            }
            return -1;
        }

        // This function just updates all the coin levels in the list
        public void UpdateData(log4net.ILog log = null)
        {
            foreach (ChannelData d in m_UnitDataList)
            {
                d.Level = CheckCoinLevel(d.Value, d.Currency, log);
                _inventory.UpdateInventory(InventarioCash.Location.SMARTHOPPER, d.Value, InventarioCash.TipoOperacion.reemplazar, d.Level);

                IsCoinRecycling(d.Value, d.Currency, ref d.Recycling);
            }

            _inventory.SaveInventory();
            
        }

        // This function uses the GET ROUTING command to see if a specified coin is recycling. The
        // caller passes a bool across which is set by the function.
        public void IsCoinRecycling(int coinValue, char[] currency, ref bool response, log4net.ILog log = null)
        {
            // First determine if the coin is currently being recycled
            m_cmd.CommandData[0] = CCommands.SSP_CMD_GET_DENOMINATION_ROUTE;
            byte[] b = CHelpers.ConvertInt32ToBytes(coinValue);
            m_cmd.CommandData[1] = b[0];
            m_cmd.CommandData[2] = b[1];
            m_cmd.CommandData[3] = b[2];
            m_cmd.CommandData[4] = b[3];

            // Add currency
            m_cmd.CommandData[5] = (byte)currency[0];
            m_cmd.CommandData[6] = (byte)currency[1];
            m_cmd.CommandData[7] = (byte)currency[2];
            m_cmd.CommandDataLength = 8;

            if (!SendCommand(log)) return;
            if (CheckGenericResponses(log))
            {
                // True if it is currently being recycled
                if (m_cmd.ResponseData[1] == 0x00)
                {
                    response = true;
                    if (log != null)
                        log.Debug(CHelpers.FormatToCurrency(coinValue) + " is recycling\r\n");
                }
                // False if not
                else if (m_cmd.ResponseData[1] == 0x01)
                {
                    response = false;
                    if (log != null)
                        log.Debug(CHelpers.FormatToCurrency(coinValue) + " is not recycling\r\n");
                }
            }
        }

        // This function returns a member of the internal array to check whether a channel is
        // recycling, this can't be called before setup request or it will be inaccurate.
        public bool IsChannelRecycling(int channel, log4net.ILog log = null)
        {
            if (channel > 0 && channel <= m_NumberOfChannels)
            {
                foreach (ChannelData d in m_UnitDataList)
                {
                    if (d.Channel == channel)
                    {
                        if (log != null) log.Debug("Channel " + channel + " recycling status: " + d.Recycling.ToString() + "\r\n");
                        return d.Recycling;
                    }
                }
            }
            return false;
        }

        // This function gets the CASHBOX PAYOUT OPERATION DATA from the hopper and returns it as a string.
        // It can be called after SMARTevents such as SMART empty.
        public string GetCashboxPayoutOpData(log4net.ILog log = null)
        {
            StringBuilder sbDisplay = new StringBuilder(100);
            // first send the command
            m_cmd.CommandData[0] = CCommands.SSP_CMD_CASHBOX_PAYOUT_OPERATION_DATA;
            m_cmd.CommandDataLength = 1;

            if (!SendCommand(log)) return "";

            // now deal with the response data
            if (CheckGenericResponses(log))
            {
                // number of different coins
                int numberOfDenominations = m_cmd.ResponseData[1];

                sbDisplay.Append("Total Number of Coins: ");
                sbDisplay.Append(numberOfDenominations.ToString());
                sbDisplay.AppendLine();
                sbDisplay.AppendLine();

                int i = 0;
                for (i = 2; i < (9 * numberOfDenominations); i += 9)
                {
                    sbDisplay.Append("Moved ");
                    sbDisplay.Append(CHelpers.ConvertBytesToInt16(m_cmd.ResponseData, i));
                    sbDisplay.Append(" x ");
                    sbDisplay.Append(CHelpers.FormatToCurrency(CHelpers.ConvertBytesToInt32(m_cmd.ResponseData, i + 2)));
                    sbDisplay.Append(" ");
                    sbDisplay.Append((char)m_cmd.ResponseData[i + 6]);
                    sbDisplay.Append((char)m_cmd.ResponseData[i + 7]);
                    sbDisplay.Append((char)m_cmd.ResponseData[i + 8]);
                    sbDisplay.Append(" to cashbox");
                    sbDisplay.AppendLine();
                }

                sbDisplay.Append(CHelpers.ConvertBytesToInt32(m_cmd.ResponseData, i));
                sbDisplay.Append(" coins not recognised");
                sbDisplay.AppendLine();

                if (log != null) log.Debug(sbDisplay.ToString());
                return sbDisplay.ToString();
            }
            return "";
        }

        // This function uses the FLOAT AMOUNT command to set the float amount. The Hopper will empty
        // coins into the cashbox leaving the requested floating amount in the payout. The minimum payout
        // is also setup so the hopper will leave itself the ability to payout the minimum value requested.
        public bool SetFloat(short minPayout, int floatAmount, char[] currency, log4net.ILog log = null)
        {
            m_cmd.CommandData[0] = CCommands.SSP_CMD_FLOAT_AMOUNT;

            // Min payout
            byte[] b = CHelpers.ConvertInt16ToBytes(minPayout);
            m_cmd.CommandData[1] = b[0];
            m_cmd.CommandData[2] = b[1];

            // Amount to payout
            b = CHelpers.ConvertInt32ToBytes(floatAmount);
            m_cmd.CommandData[3] = b[0];
            m_cmd.CommandData[4] = b[1];
            m_cmd.CommandData[5] = b[2];
            m_cmd.CommandData[6] = b[3];

            // Country code
            m_cmd.CommandData[7] = (byte)currency[0];
            m_cmd.CommandData[8] = (byte)currency[1];
            m_cmd.CommandData[9] = (byte)currency[2];

            m_cmd.CommandData[10] = 0x58; // real float

            m_cmd.CommandDataLength = 11;

            if (!SendCommand(log)) return false;

            if (CheckGenericResponses(log))
            {
                if (log != null)
                    log.Debug("Set float successfully\r\n");
                return true;
            }
            return false;
        }

        // This function sets the protocol version using the command HOST PROTOCOL VERSION.
        public bool SetProtocolVersion(byte b, log4net.ILog log = null)
        {
            m_cmd.CommandData[0] = CCommands.SSP_CMD_HOST_PROTOCOL_VERSION;
            m_cmd.CommandData[1] = b;
            m_cmd.CommandDataLength = 2;

            if (!SendCommand(log)) return false;
            if (CheckGenericResponses(log))
            {
                if (log != null) log.Debug("Setting protocol version " + b.ToString() + "\r\n");
            }
            return true;
        }

        // This function opens the com port identified in the command structure, using the SSP library.
        public bool OpenComPort(log4net.ILog log = null)
        {
            // open com port
            if (log != null) log.Debug("Opening com port\r\n");
            if (m_eSSP.OpenSSPComPort(m_cmd) == false)
            {
                return false;
            }
            return true;
        }

        // This function performs a number of commands in order to setup the encryption between the host and the hopper.
        public bool NegotiateKeys(log4net.ILog log = null)
        {
            // make sure encryption is off
            m_cmd.EncryptionStatus = false;

            // send sync
            if (log != null) log.Debug("Syncing... ");
            m_cmd.CommandData[0] = CCommands.SSP_CMD_SYNC;
            m_cmd.CommandDataLength = 1;

            if (!SendCommand(log)) return false;
            if (log != null) log.Debug("Success");

            m_eSSP.InitiateSSPHostKeys(keys, m_cmd);

            // send generator
            m_cmd.CommandData[0] = CCommands.SSP_CMD_SET_GENERATOR;
            m_cmd.CommandDataLength = 9;
            if (log != null) log.Debug("Setting generator... ");

            // Convert generator to bytes and add to command data.
            BitConverter.GetBytes(keys.Generator).CopyTo(m_cmd.CommandData, 1);

            if (!SendCommand(log)) return false;
            if (log != null) log.Debug("Success\r\n");

            // send modulus
            m_cmd.CommandData[0] = CCommands.SSP_CMD_SET_MODULUS;
            m_cmd.CommandDataLength = 9;
            if (log != null) log.Debug("Sending modulus... ");

            // Convert modulus to bytes and add to command data.
            BitConverter.GetBytes(keys.Modulus).CopyTo(m_cmd.CommandData, 1);

            if (!SendCommand(log)) return false;
            if (log != null) log.Debug("Success\r\n");

            // send key exchange
            m_cmd.CommandData[0] = CCommands.SSP_CMD_REQUEST_KEY_EXCHANGE;
            m_cmd.CommandDataLength = 9;
            if (log != null) log.Debug("Exchanging keys... ");

            // Convert host intermediate key to bytes and add to command data.
            BitConverter.GetBytes(keys.HostInter).CopyTo(m_cmd.CommandData, 1);


            if (!SendCommand(log)) return false;
            if (log != null) log.Debug("Success\r\n");

            // Read slave intermediate key.
            keys.SlaveInterKey = BitConverter.ToUInt64(m_cmd.ResponseData, 1);

            m_eSSP.CreateSSPHostEncryptionKey(keys);

            // get full encryption key
            m_cmd.Key.FixedKey = 0x0123456701234567;
            m_cmd.Key.VariableKey = keys.KeyHost;

            if (log != null) log.Debug("Keys successfully negotiated\r\n");

            return true;
        }

        // This function uses the setup request command to get all the information about the hopper. It can optionally
        // output to a specified textbox.
        public void HopperSetupRequest(log4net.ILog log = null)
        {
            StringBuilder sbDisplay = new StringBuilder(1000);

            // send setup request
            m_cmd.CommandData[0] = CCommands.SSP_CMD_SETUP_REQUEST;
            m_cmd.CommandDataLength = 1;

            if (!SendCommand(null)) return;

            // display setup request


            // unit type
            int index = 1;
            sbDisplay.Append("Unit Type: ");
            m_UnitType = (char)m_cmd.ResponseData[index++];
            switch (m_UnitType)
            {
                case (char)0x00: sbDisplay.Append("Validator"); break;
                case (char)0x03: sbDisplay.Append("SMART Hopper"); break;
                case (char)0x06: sbDisplay.Append("SMART Payout"); break;
                case (char)0x07: sbDisplay.Append("NV11"); break;
                default: sbDisplay.Append("Unknown Type"); break;
            }

            // firmware
            sbDisplay.AppendLine();
            sbDisplay.Append("Firmware: ");
            while (index <= 5)
            {
                sbDisplay.Append((char)m_cmd.ResponseData[index++]);
                if (index == 4)
                    sbDisplay.Append(".");
            }
            sbDisplay.AppendLine();

            // country code.
            // legacy code so skip it.
            index += 3;

            // protocol version
            sbDisplay.Append("Protocol Version: ");
            m_ProtocolVersion = m_cmd.ResponseData[index++];
            sbDisplay.Append(m_ProtocolVersion);
            sbDisplay.AppendLine();

            // number of coin values
            m_NumberOfChannels = m_cmd.ResponseData[index++];
            sbDisplay.Append("Number of Coin Values: " + m_NumberOfChannels);
            sbDisplay.AppendLine();

            // channel denominations

            sbDisplay.AppendLine();
            sbDisplay.Append("Channel Denominations");
            sbDisplay.AppendLine();

            // Add channel data to list then display.
            // Clear list.
            m_UnitDataList.Clear();
            for (byte i = 0; i < m_NumberOfChannels; i++)
            {
                ChannelData loopChannelData = new ChannelData();

                // Channel number.
                loopChannelData.Channel = (byte)(i + 1);

                // Channel value.
                loopChannelData.Value = BitConverter.ToInt16(m_cmd.ResponseData, index + (i * 2));

                // Channel currency.

                loopChannelData.Currency[0] = (char)m_cmd.ResponseData[index + (2 * (m_NumberOfChannels) + (i * 3))];
                loopChannelData.Currency[1] = (char)m_cmd.ResponseData[(index + 1) + (2 * (m_NumberOfChannels) + (i * 3))];
                loopChannelData.Currency[2] = (char)m_cmd.ResponseData[(index + 2) + (2 * (m_NumberOfChannels) + (i * 3))];

                // Channel level.
                loopChannelData.Level = CheckCoinLevel(loopChannelData.Value, loopChannelData.Currency);

                IsCoinRecycling(loopChannelData.Value, loopChannelData.Currency, ref loopChannelData.Recycling);

                // Add data to list.
                m_UnitDataList.Add(loopChannelData);

                //Display data
                sbDisplay.Append("Channel ");
                sbDisplay.Append(loopChannelData.Channel);
                sbDisplay.Append(": ");
                sbDisplay.Append(loopChannelData.Value / 100f);
                sbDisplay.Append(" ");
                sbDisplay.Append(loopChannelData.Currency);
                sbDisplay.AppendLine();
            }

            sbDisplay.AppendLine();

            // Sort the list by .Value
            m_UnitDataList.Sort((d1, d2) => d1.Value.CompareTo(d2.Value));


            if (log != null)
                log.Debug(sbDisplay.ToString());
        }

        // This function clears the inhibit on all channels.
        public void SetInhibits(log4net.ILog log = null)
        {
            for (int i = 1; i <= m_NumberOfChannels; i++)
            {
                SetChannelInhibit(i, false, log);
            }
        }

        // This function sends the set coin mech inhibits command to set whether a coin is accepted into the Hopper.
        // Please note: The response data of this command if it is sent with no coin mech attached will be
        // WRONG PARAMETERS.
        public void SetChannelInhibit(int channel, bool inhibit, log4net.ILog log = null)
        {
            // set or clears inhibit on channel

            m_cmd.CommandData[0] = CCommands.SSP_CMD_SET_COIN_MECH_INHIBITS;
            if (inhibit)
            {
                m_cmd.CommandData[1] = 0x0; // coin inhibited
            }
            else
            {
                m_cmd.CommandData[1] = 0x01; // coin accepted
            }

            // currency
            foreach (ChannelData d in m_UnitDataList)
            {
                if (d.Channel == channel)
                {
                    m_cmd.CommandData[2] = (byte)d.Value;
                    m_cmd.CommandData[3] = 0x00;

                    m_cmd.CommandData[4] = (byte)d.Currency[0];
                    m_cmd.CommandData[5] = (byte)d.Currency[1];
                    m_cmd.CommandData[6] = (byte)d.Currency[2];
                    break;
                }
            }
            m_cmd.CommandDataLength = 7;

            if (!SendCommand(log)) return;
            if (CheckGenericResponses(log) && log != null)
            {
                if (inhibit)
                {
                    log.Debug("Inhibit set on channel " + channel.ToString() + "\r\n");
                }
                else
                {
                    log.Debug("Inhibit cleared on channel " + channel.ToString() + "\r\n");
                }
            }
        }

        // This function gets the serial number of the device.  An optional Device parameter can be used
        // for TEBS systems to specify which device's serial number should be returned.
        // 0x00 = NV200
        // 0x01 = SMART Payout
        // 0x02 = Tamper Evident Cash Box.
        public void GetSerialNumber(byte Device, log4net.ILog log = null)
        {
            m_cmd.CommandData[0] = CCommands.SSP_CMD_GET_SERIAL_NUMBER;
            m_cmd.CommandData[1] = Device;
            m_cmd.CommandDataLength = 2;


            if (!SendCommand(log)) return;
            if (CheckGenericResponses(log) && log != null)
            {
                // Response data is big endian, so reverse bytes 1 to 4.
                Array.Reverse(m_cmd.ResponseData, 1, 4);
                log.Debug("Serial Number Device " + Device + ": ");
                log.Debug(BitConverter.ToUInt32(m_cmd.ResponseData, 1).ToString());
                log.Debug("\r\n");
            }
        }

        public void GetSerialNumber(log4net.ILog log = null)
        {
            m_cmd.CommandData[0] = CCommands.SSP_CMD_GET_SERIAL_NUMBER;
            m_cmd.CommandDataLength = 1;

            if (!SendCommand(log)) return;
            if (CheckGenericResponses(log) && log != null)
            {
                // Response data is big endian, so reverse bytes 1 to 4.
                Array.Reverse(m_cmd.ResponseData, 1, 4);
                log.Debug("Serial Number ");
                log.Debug(": ");
                log.Debug(BitConverter.ToUInt32(m_cmd.ResponseData, 1).ToString());
                log.Debug("\r\n");
            }
        }

        // This function is called repeatedly to poll the hopper about what events are happening. It
        // can optionally output these events to a textbox.
        public PollResponse DoPoll(log4net.ILog log)
        {

            PollResponse Resp = new PollResponse();
            Resp.Success = false;
            byte i;

            //send poll
            m_cmd.CommandData[0] = CCommands.SSP_CMD_POLL;
            m_cmd.CommandDataLength = 1;
            
            if (!SendCommand(log)) return Resp;


            // if FA is received it indicates the unit briefly lost power and the key so return false
            // for a full reconnection
            if (m_cmd.ResponseData[0] == 0xFA)
                return Resp;

            // isolate poll response to allow reuse of the SSP_COMMAND structure
            byte[] response = new byte[256];
            byte responseLength = m_cmd.ResponseDataLength;
            m_cmd.ResponseData.CopyTo(response, 0);

            //parse poll response
            int coin = 0;
            string currency = "";
            for (i = 1; i < responseLength; i++)
            {
                switch (response[i])
                {
                    // This response indicates that the unit was reset and this is the first time a poll
                    // has been called since the reset.
                    case CCommands.SSP_POLL_SLAVE_RESET:
                        UpdateData();
                        Resp.Comand = Commandos.SSP_POLL_SLAVE_RESET;
                        break;
                    // This response is given when the unit is disabled.
                    case CCommands.SSP_POLL_DISABLED:
                        Resp.Comand = Commandos.SSP_POLL_DISABLED;
                        log.Debug("Unit disabled...\r\n");
                        break;
                    // The unit is in the process of paying out a coin or series of coins, this will continue to poll
                    // until the coins have been fully dispensed
                    case CCommands.SSP_POLL_DISPENSING:
                        log.Debug("Dispensing coin(s)...\r\n");
                        Resp.Comand = Commandos.SSP_POLL_DISPENSING;
                        // Now the index needs to be moved on to skip over the data provided by this response so it
                        // is not parsed as a normal poll response.
                        // In this response, the data includes the number of countries being dispensed (1 byte), then a 4 byte value
                        // and 3 byte currency code for each country. 
                        i += (byte)((response[i + 1] * 7) + 1);
                        break;
                    // This is polled when a unit has finished a dispense operation. The following 4 bytes give the 
                    // value of the coin(s) dispensed.
                    case CCommands.SSP_POLL_DISPENSED:
                        for (int j = 0; j < response[i + 1] * 7; j += 7)
                        {
                            coin = CHelpers.ConvertBytesToInt32(m_cmd.ResponseData, i + j + 2); // get coin data from response
                            // get currency from response
                            currency = "";
                            currency += (char)response[i + j + 6];
                            currency += (char)response[i + j + 7];
                            currency += (char)response[i + j + 8];
                            log.Debug(CHelpers.FormatToCurrency(coin) + " " + currency + " coin(s) dispensed\r\n");
                        }

                        UpdateData();
                        EnableHopper();
                        _evnt.GetEvent<UpdateInventory>().Publish(true);
                        i += (byte)((response[i + 1] * 7) + 1);
                        Resp.Comand = Commandos.SSP_POLL_DISPENSED;
                        break;
                    // The coins being recycled inside the unit are running low.
                    //case CCommands.SSP_POLL_COINS_LOW:
                    //    log.Debug("Coins low\r\n");
                    //    break;
                    // There are no coins left in the unit.
                    //case CCommands.SSP_POLL_EMPTY:
                    //    log.Debug("Unit empty\r\n");
                    //    break;
                    // The mechanism inside the unit is jammed.
                    case CCommands.SSP_POLL_JAMMED:
                        log.Debug("Jammed\r\n");
                        i += (byte)((response[i + 1] * 7) + 1);
                        Resp.Comand = Commandos.SSP_POLL_JAMMED;
                        break;
                    // A dispense, SMART empty or float operation has been halted.
                    case CCommands.SSP_POLL_HALTED:
                        i += (byte)((response[i + 1] * 7) + 1);
                        Resp.Comand = Commandos.SSP_POLL_HALTED;
                        break;
                    // The device is 'floating' a specified amount of coins. It will transfer some to the cashbox and
                    // leave the specified amount in the device.
                    case CCommands.SSP_POLL_FLOATING:
                        i += (byte)((response[i + 1] * 7) + 1);
                        Resp.Comand = Commandos.SSP_POLL_FLOATING;
                        break;
                    // The float operation has completed.
                    case CCommands.SSP_POLL_FLOATED:
                        UpdateData();
                        EnableHopper();
                        i += (byte)((response[i + 1] * 7) + 1);
                        Resp.Comand = Commandos.SSP_POLL_FLOATED;
                        break;
                    // This poll appears when the SMART Hopper has been searching for a coin but cannot find it within
                    // the timeout period.
                    case CCommands.SSP_POLL_TIME_OUT:
                        log.Debug("Search for suitable coins failed\r\n");
                        i += (byte)((response[i + 1] * 7) + 1);
                        Resp.Comand = Commandos.SSP_POLL_TIME_OUT;
                        break;
                    // A payout was interrupted in some way. The amount paid out does not match what was requested. The value
                    // of the dispensed and requested amount is contained in the response.
                    case CCommands.SSP_POLL_INCOMPLETE_PAYOUT:
                        log.Debug("Incomplete payout detected...\r\n");
                        i += (byte)((response[i + 1] * 11) + 1);
                        Resp.Comand = Commandos.SSP_POLL_INCOMPLETE_PAYOUT;
                        break;
                    // A float was interrupted in some way. The amount floated does not match what was requested. The value
                    // of the dispensed and requested amount is contained in the response.
                    case CCommands.SSP_POLL_INCOMPLETE_FLOAT:
                        log.Debug("Incomplete float detected...\r\n");
                        i += (byte)((response[i + 1] * 11) + 1);
                        Resp.Comand = Commandos.SSP_POLL_INCOMPLETE_FLOAT;
                        break;
                    // This poll appears when coins have been dropped to the cashbox whilst making a payout. The value of
                    // coins and the currency is reported in the response.
                    case CCommands.SSP_POLL_CASHBOX_PAID:
                        log.Debug("Cashbox paid\r\n");
                        i += (byte)((response[i + 1] * 7) + 1);
                        Resp.Comand = Commandos.SSP_POLL_CASHBOX_PAID;
                        break;
                    // A credit event has been detected, this is when the coin mech has accepted a coin as legal currency.
                    case CCommands.SSP_POLL_COIN_CREDIT:
                        coin = CHelpers.ConvertBytesToInt16(m_cmd.ResponseData, i + 1);

                        _evnt.GetEvent<Cash_credited>().Publish(coin);
                        currency = "";
                        currency += (char)response[i + 5];
                        currency += (char)response[i + 6];
                        currency += (char)response[i + 7];
                        log.Debug(CHelpers.FormatToCurrency(coin) + " " + currency + " credited\r\n");
                        UpdateData();
                        i += 7;
                        Resp.Comand = Commandos.SSP_POLL_COIN_CREDIT;
                        break;
                    // The coin mech has become jammed.
                    case CCommands.SSP_POLL_COIN_MECH_JAMMED:
                        log.Debug("Coin mech jammed\r\n");
                        Resp.Comand = Commandos.SSP_POLL_COIN_MECH_JAMMED;
                        break;
                    // The return button on the coin mech has been pressed.
                    case CCommands.SSP_POLL_COIN_MECH_RETURN_PRESSED:
                        log.Debug("Return button pressed\r\n");
                        Resp.Comand = Commandos.SSP_POLL_COIN_MECH_RETURN_PRESSED;
                        break;
                    // The unit is in the process of dumping all the coins stored inside it into the cashbox.
                    case CCommands.SSP_POLL_EMPTYING:
                        log.Debug("Emptying...\r\n");
                        Resp.Comand = Commandos.SSP_POLL_EMPTYING;
                        break;
                    // The unit has finished dumping coins to the cashbox.
                    case CCommands.SSP_POLL_EMPTIED:
                        log.Debug("Emptied\r\n");
                        UpdateData();
                        EnableHopper();
                        Resp.Comand = Commandos.SSP_POLL_EMPTIED;
                        break;
                    // A fraud attempt has been detected.
                    case CCommands.SSP_POLL_FRAUD_ATTEMPT:
                        log.Debug("Fraud attempted\r\n");
                        i += (byte)((response[i + 1] * 7) + 1);
                        Resp.Comand = Commandos.SSP_POLL_FRAUD_ATTEMPT;
                        break;
                    // The unit is in the process of dumping all the coins stored inside it into the cashbox.
                    // This poll means that the unit is keeping track of what it empties.
                    case CCommands.SSP_POLL_SMART_EMPTYING:
                        log.Debug("SMART emptying...\r\n");
                        i += (byte)((response[i + 1] * 7) + 1);
                        Resp.Comand = Commandos.SSP_POLL_SMART_EMPTYING;
                        break;
                    // The unit has finished SMART emptying. The info on what has been dumped can be obtained
                    // by sending the CASHBOX PAYOUT OPERATION DATA command.
                    case CCommands.SSP_POLL_SMART_EMPTIED:
                        GetCashboxPayoutOpData(log);
                        UpdateData();
                        EnableHopper();
                        log.Debug("SMART emptied\r\n");
                        i += (byte)((response[i + 1] * 7) + 1);
                        Resp.Comand = Commandos.SSP_POLL_SMART_EMPTIED;
                        break;
                    // A coin has had its routing changed to either cashbox or recycling.
                    //case CCommands.SSP_POLL_COIN_ROUTED:
                    //    log.Debug("Routed coin\r\n");
                    //    UpdateData();
                    //    break;
                    default:
                        log.Debug("Unsupported poll response received: " + (int)response[i] + "\r\n");
                        break;
                }
            }
            Resp.Success = true;
            return Resp;
        }

        // Non-Command functions 

        // This is used to send a command via SSP to the hopper
        public bool SendCommand(log4net.ILog log)
        {
            // attempt to send the command
            if (m_eSSP.SSPSendCommand(m_cmd, info) == false)
            {
                m_eSSP.CloseComPort();
                //m_Comms.UpdateLog(info, true);
                if (log != null) log.Error("Sending command failed\r\nPort status: " + m_cmd.ResponseStatus.ToString() + "\r\n");
                return false;
            }
            //// update the log after every command
           // m_Comms.UpdateLog(info);
            return true;
        }

        // This returns the currency of a specified channel.
        public char[] GetChannelCurrency(int channel)
        {
            if (channel > 0 && channel <= m_NumberOfChannels)
            {
                foreach (ChannelData d in m_UnitDataList)
                {
                    if (d.Channel == channel)
                        return d.Currency;
                }
            }
            return null;
        }

        // This returns the level of a specified channel.
        public int GetChannelLevel(int channel)
        {
            if (channel > 0 && channel <= m_NumberOfChannels)
            {
                foreach (ChannelData d in m_UnitDataList)
                {
                    if (d.Channel == channel)
                        return d.Level;
                }
            }
            return -1;
        }

        // This is similar to the above function but instead returns all the channels as a
        // nicely formatted string
        public string GetChannelLevelInfo()
        {
            string s = "";

            foreach (ChannelData d in m_UnitDataList)
            {
                s += CHelpers.FormatToCurrency(d.Value) + " " + d.Currency[0] + d.Currency[1] + d.Currency[2];
                s += " [" + d.Level + "] = " + ((d.Level * d.Value) / 100f).ToString();
                s += " " + d.Currency[0] + d.Currency[1] + d.Currency[2] + "\r\n";
            }
            return s;
        }

        // This takes a coin value and returns the channel number
        public int GetChannelofCoin(int coin)
        {
            foreach (ChannelData d in m_UnitDataList)
            {
                if (d.Value == coin)
                    return d.Channel;
            }
            return -1;
        }

        // Exception and Error Handling

        // This is used for generic response error catching, it outputs the info in a
        // meaningful way.
        public bool CheckGenericResponses(log4net.ILog log = null)
        {
            if (m_cmd.ResponseData[0] == CCommands.SSP_RESPONSE_OK)
                return true;
            else
            {
                if (log != null)
                {
                    switch (m_cmd.ResponseData[0])
                    {
                        case CCommands.SSP_RESPONSE_COMMAND_CANNOT_BE_PROCESSED:
                            if (m_cmd.ResponseData[1] == 0x03)
                            {
                                log.Debug("Unit responded with a \"Busy\" response, command cannot be " +
                                    "processed at this time\r\n");
                            }
                            else
                            {
                                log.Debug("Command response is CANNOT PROCESS COMMAND, error code - 0x"
                                + BitConverter.ToString(m_cmd.ResponseData, 1, 1) + "\r\n");
                            }
                            return false;
                        case CCommands.SSP_RESPONSE_FAIL:
                            log.Debug("Command response is FAIL\r\n");
                            return false;
                        case CCommands.SSP_RESPONSE_KEY_NOT_SET:
                            log.Debug("Command response is KEY NOT SET\r\n");
                            return false;
                        case CCommands.SSP_RESPONSE_PARAMETER_OUT_OF_RANGE:
                            log.Debug("Command response is PARAM OUT OF RANGE\r\n");
                            return false;
                        case CCommands.SSP_RESPONSE_SOFTWARE_ERROR:
                            log.Debug("Command response is SOFTWARE ERROR\r\n");
                            return false;
                        case CCommands.SSP_RESPONSE_COMMAND_NOT_KNOWN:
                            log.Debug("Command response is UNKNOWN\r\n");
                            return false;
                        case CCommands.SSP_RESPONSE_WRONG_NO_PARAMETERS:
                            log.Debug("Command response is WRONG PARAMETERS\r\n");
                            return false;
                        default:
                            return false;
                    }
                }
                else
                    return false;
            }
        }
    };
}