// Copyright 2008 Louis DeJardin - http://whereslou.com
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 
using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace ClientRenderingViews.Models
{
    public class CartRepository
    {

        static readonly Dictionary<Guid, Cart> _carts = new Dictionary<Guid, Cart>();

        public Cart GetCurrentCart(Guid cartId)
        {
            lock(_carts)
            {
                Cart cart;
                if (!_carts.TryGetValue(cartId, out cart))
                {
                    cart = new Cart();
                    _carts[cart.Id] = cart;
                }
                return cart;
            }
        }

        public Cart Reset(Guid cartId)
        {
            lock(_carts)
            {
                if (_carts.ContainsKey(cartId))
                    _carts.Remove(cartId);
                return GetCurrentCart(Guid.Empty);
            }
        }
    }
}